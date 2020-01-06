using System.Collections.Generic;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery
{
    public class CustomeQueryFieldSupportedOption : ValueObject<CustomeQueryFieldSupportedOption>
    {
        public CustomeQueryFieldSupportedOption(CustomeQueryField field, IList<string> supportedRelationalOperators)
        {
            Field = field;
            SupportedRelationalOperators = supportedRelationalOperators;
        }

        /// <summary>
        /// 字段信息
        /// </summary>
        public CustomeQueryField Field { get; private set; }

        /// <summary>
        /// 可以支持关系运算符
        /// </summary>
        public IList<string> SupportedRelationalOperators { get; private set; }
    }
}
