using Autofac;
using MySvc.DotNetCore.Framework.Infrastructure.Adapter.AutoMapper;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using MySvc.DotNetCore.Framework.Infrastructure.Json.NewtonsoftJson;

namespace Sample.Product.Api.DI.AutofacModules
{
    /// <summary>
    /// 公共模块的依赖注入
    /// </summary>
    public class CommonModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //JSON转换器
            builder.RegisterType<NewtonsoftJsonConverter>().As<IJsonConverter>().SingleInstance();

            //注册类型适配转换器
            builder.RegisterType<AutomapperTypeAdapter>().As<ITypeAdapter>().SingleInstance();

        }
    }
}
