using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 获取程序集已加载的类型
        /// </summary>
        /// <param name="assembly">程序集对象</param>
        /// <returns>已加载的类型</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        
    }
}