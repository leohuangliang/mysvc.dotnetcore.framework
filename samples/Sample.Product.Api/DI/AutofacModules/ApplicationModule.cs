using System.Reflection;
using Autofac;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.IntegrationEventService;
using Sample.Product.Application.IntegrationEvents.EventHandling;
using Sample.Product.Application.Queries;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 应用层相关的依赖注入配置
    /// </summary>
    public class ApplicationModule : Autofac.Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // Register the Queries
            builder.RegisterType<ProductQueries>().As<IProductQueries>().InstancePerLifetimeScope();

            builder.RegisterType<IntegrationEventService>().As<IIntegrationEventService>().InstancePerLifetimeScope();
        }
    }
}
