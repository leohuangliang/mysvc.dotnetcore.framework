using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Core.Specification;

namespace Capmarvel.Framework.Domain.Common.AggregatesModel.CustomQueryAggregate.Specifications
{
    /// <summary>
    /// 根据模版名称完整匹配自定义查询组的规格
    /// </summary>
    public class MatchByNameEqualSpecification : Specification<CustomQuery>
    {
        public readonly string _name;

        public MatchByNameEqualSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<CustomQuery, bool>> GetExpression()
        {
            return x => x.Name == _name;
        }
    }
}
