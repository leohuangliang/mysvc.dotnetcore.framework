﻿using Autofac;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using MySvc.Framework.Infrastructure.NewtonsoftJson;

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


        }
    }
}
