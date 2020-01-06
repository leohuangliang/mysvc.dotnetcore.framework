using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Models;
using Capmarvel.Framework.Domain.Core.Specification;

namespace Capmarvel.Framework.Domain.Common.AggregatesModel.CustomQueryAggregate.Specifications
{
    /// <summary>
    /// 根据账户匹配自定义查询的规格
    /// </summary>
    public class MatchByAccountSpecification : Specification<CustomQuery>
    {
        public readonly AccountInfo _accountInfo;

        private readonly bool _isOnlyTenantCode;

        public MatchByAccountSpecification(AccountInfo accountInfo, bool isOnlyTenantCode = true)
        {
            _accountInfo = accountInfo;
            _isOnlyTenantCode = isOnlyTenantCode;
        }

        public override Expression<Func<CustomQuery, bool>> GetExpression()
        {
            if (_isOnlyTenantCode)
            {
                return x => x.AccountInfo.TenantCode == _accountInfo.TenantCode;
            }

            return x => x.AccountInfo.TenantCode == _accountInfo.TenantCode 
                        && x.AccountInfo.UserName == _accountInfo.UserName;
        }
    }
}
