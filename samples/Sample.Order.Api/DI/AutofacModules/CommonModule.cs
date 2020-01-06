using Autofac;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Adapter.AutoMapper;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Cap;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Json.NewtonsoftJson;

namespace Sample.Order.Api.DI.AutofacModules
{
    /// <summary>
    /// 公共模块的依赖注入
    /// </summary>
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //JSON转换器
            builder.RegisterType<NewtonsoftJsonConverter>().As<IJsonConverter>().SingleInstance();

            //注册类型适配转换器
            builder.RegisterType<AutomapperTypeAdapter>().As<ITypeAdapter>().SingleInstance();


            builder.RegisterType<CapEventBus>().As<IEventBus>()
                .SingleInstance();
        }
    }
}
