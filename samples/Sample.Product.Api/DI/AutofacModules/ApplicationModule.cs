using System.Reflection;
using Autofac;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using Sample.Product.Application.IntegrationEvents.EventHandling;
using Sample.Product.Application.Queries;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 应用层相关的依赖注入配置
    /// </summary>
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register the Queries
            builder.RegisterType<ProductQueries>().As<IProductQueries>().InstancePerLifetimeScope();

            //// Register the Integration Event Handler
            //builder.RegisterAssemblyTypes(typeof(OrderCreatedIntegrationEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
