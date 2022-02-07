using Autofac;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.Framework.Infrastructure.IntegrationEventService;
using Sample.Order.Application.Queries;

namespace Sample.Order.Api.DI.AutofacModules
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
            builder.RegisterType<OrderQueries>().As<IOrderQueries>().InstancePerLifetimeScope();

            builder.RegisterType<IntegrationEventService>().As<IIntegrationEventService>().InstancePerLifetimeScope();
        }
    }
}
