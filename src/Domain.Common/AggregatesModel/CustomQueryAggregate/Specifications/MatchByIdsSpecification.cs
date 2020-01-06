using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Core.Specification;

namespace Capmarvel.Framework.Domain.Common.AggregatesModel.CustomQueryAggregate.Specifications
{
    /// <summary>
    /// 根据多个查询组Id匹配自定义查询组的规格
    /// </summary>
    public class MatchByIdsSpecification : Specification<CustomQuery>
    {
        public readonly IList<string> _ids;

        public MatchByIdsSpecification(IList<string> ids)
        {
            _ids = ids;
        }

        public override Expression<Func<CustomQuery, bool>> GetExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}
