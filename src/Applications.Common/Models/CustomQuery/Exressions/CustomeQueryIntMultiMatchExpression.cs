﻿using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-整数字段值与多个整数集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryIntMultiMatchExpression : CustomeQueryMultiMatchExpression<int>
    {
        public CustomeQueryIntMultiMatchExpression()
        {
            Type = CustomeQueryExpressionType.INT_MULTI_MATCH;
        }
    }
}