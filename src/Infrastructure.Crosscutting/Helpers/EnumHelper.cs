using System;
using System.ComponentModel;
using System.Reflection;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 
    /// </summary>

    public static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetName(this Enum em)
        {
            return Enum.GetName(em.GetType(), em);
        }
    }
}
