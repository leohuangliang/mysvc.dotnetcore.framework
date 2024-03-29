﻿using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Options;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using Sample.Product.Domain.Repositories;
using Sample.Product.Repository.MongoDB;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 仓储相关的依赖注入模块
    /// </summary>
    public class RepositoryModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDBManager>().SingleInstance();
            builder.RegisterType<EntityIdGenerator>()
                .As<IEntityIdGenerator>().SingleInstance();

            //默认用于读写
            builder.RegisterType<MongoDBContext>()
                .As<IMongoDBContext, IDBContext>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IOptions<MongoDBSettings>),
                    (pi, ctx) => Options.Create(ctx.Resolve<IOptionsMonitor<MongoDBSettings>>().Get("MongoDBForWrite"))))
                .InstancePerLifetimeScope();
            //用于只读
            builder.RegisterType<MongoDBContext>()
                .Named<IMongoDBContext>("ReadOnly")
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IOptions<MongoDBSettings>),
                    (pi, ctx) => Options.Create(ctx.Resolve<IOptionsMonitor<MongoDBSettings>>().Get("MongoDBForRead"))))
                .InstancePerLifetimeScope();

            /*
             * 配置领域仓储
             */
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<IntegrationEventLogRepository>().As<IIntegrationEventLogRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IntegrationEventLogManager>().As<IIntegrationEventLogManager>()
                .InstancePerLifetimeScope();

            /*
             * 配置只读仓储
             * 若构造的参数是IMongoDBContext， 则解析Name为ReadOnly的
             */
            builder.RegisterType<ProductReadOnlyRepository>().As<IProductReadOnlyRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IMongoDBContext>("ReadOnly")).InstancePerLifetimeScope();

        }
    }
}
