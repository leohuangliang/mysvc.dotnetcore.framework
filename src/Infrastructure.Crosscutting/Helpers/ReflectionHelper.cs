using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    public class ReflectionHelper
    {
        public static Type[] GetAllTypes(string assemblyName, Type type)
        {
            //加载程序集
            var assembly = Assembly.Load(assemblyName);

            var types = assembly.GetLoadableTypes().Where(t => t.BaseType == type).ToArray();
            return types;
        }

        
    }
}
