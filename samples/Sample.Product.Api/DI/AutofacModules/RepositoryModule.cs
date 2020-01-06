using Autofac;
using Autofac.Core;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Options;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using Microsoft.Extensions.Options;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Sample.Product.Domain.Repositories;
using Sample.Product.Repository.MongoDB;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 仓储相关的依赖注入模块
    /// </summary>
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDBManager>().SingleInstance();

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

            /*
             * 配置只读仓储
             * 若构造的参数是IMongoDBContext， 则解析Name为ReadOnly的
             */
            builder.RegisterType<ProductReadOnlyRepository>().As<IProductReadOnlyRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IMongoDBContext>("ReadOnly")).InstancePerLifetimeScope();

        }
    }
}
