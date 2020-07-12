using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Sample.Product.Application.Commands;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 配置基于中介者的实现的依赖注入
    /// </summary>
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //自动装配 IMediator 所在程序集中的所有的公共的，具体类将被注册。
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                //命令相关
                builder.RegisterAssemblyTypes(typeof(CreateProductCommand).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();

                //领域事件相关
            }

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            LoadBehaviors(builder);
        }

        /// <summary>
        /// 配置管道行为
        /// </summary>
        private void LoadBehaviors(ContainerBuilder builder)
        {
            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
