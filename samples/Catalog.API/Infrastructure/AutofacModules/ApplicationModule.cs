using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using Catalog.Domain;
using CataLog.Infrastructure.MongoDB.Repository;

namespace Catalog.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        //public string QueriesConnectionString { get; }

        //public ApplicationModule(string qconstr)
        //{
        //    QueriesConnectionString = qconstr;

        //}

        protected override void Load(ContainerBuilder builder)
        {

            //builder.Register(c => new OrderQueries(QueriesConnectionString))
            //    .As<IOrderQueries>()
            //    .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadOnlyMongoDBRepository<>))
                .As(typeof(IReadOnlyRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<OrderRepository>()
            //    .As<IOrderRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<RequestManager>()
            //    .As<IRequestManager>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
