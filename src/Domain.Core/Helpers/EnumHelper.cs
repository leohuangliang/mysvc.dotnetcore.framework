using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace MySvc.DotNetCore.Framework.Domain.Core.Helpers
{
    public static class EnumHelper
    {
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

        public static string GetName(this Enum em)
        {
            return Enum.GetName(em.GetType(), em);
        }
    }
}
