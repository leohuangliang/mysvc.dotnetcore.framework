﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace MySvc.Framework.Domain.Core.Helpers
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
        [Obsolete("已废弃,请使用MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers.EnumHelper.GetDescription", true)]
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
        [Obsolete("已废弃,请使用MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers.EnumHelper.GetName", true)]

        public static string GetName(this Enum em)
        {
            return Enum.GetName(em.GetType(), em);
        }
    }
}
