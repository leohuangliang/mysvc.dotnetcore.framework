using Autofac;

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
            // JSON 转换器已移除，现在使用 System.Text.Json


        }
    }
}
