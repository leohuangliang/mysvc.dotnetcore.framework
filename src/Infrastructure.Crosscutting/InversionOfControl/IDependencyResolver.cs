using System;
using System.Collections.Generic;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.InversionOfControl
{
    /// <summary>
    /// 依赖注入的解析器
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// 依据指定的类型，解析出其对象
        /// </summary>
        /// <typeparam name="T">需要解析的对象类型</typeparam>
        /// <returns>解析出对象</returns>
        T Resolve<T>();

        /// <summary>
        /// 依据指定的类型，解析出对象
        /// </summary>
        /// <param name="type">需要解析的对象类型</param>
        /// <typeparam name="T">需要解析的对象类型</typeparam>
        /// <returns>解析出对象</returns>
        T Resolve<T>(Type type);
        
        /// <summary>
        /// 依据指定的类型以及命名，解析出对象
        /// </summary>
        /// <param name="name">命名</param>
        /// <typeparam name="T">需要解析的对象类型</typeparam>
        /// <returns>解析出对象</returns>
        T Resolve<T>(string name);

        /// <summary>
        /// 依据指定的类型，解析出其绑定的所有对象
        /// </summary>
        /// <typeparam name="T">需要解析的对象类型</typeparam>
        /// <returns>解析出对象集合</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// 依据指定的类型以及命名，解析出其绑定的所有对象
        /// </summary>
        /// <typeparam name="T">需要解析的对象类型</typeparam>
        /// <returns>解析出对象集合</returns>
        IEnumerable<T> ResolveAll<T>(string name);
    }
}