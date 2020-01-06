using Capmarvel.Framework.Domain.Common.Models;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions;
using Capmarvel.Framework.Domain.Core.Attributes;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.AggregatesModel.CustomQueryAggregate
{
    /// <summary>
    /// 自定义查询
    /// </summary>
    [AggregateRootName("CustomQuerys")]
    public abstract class CustomQuery : AggregateRoot
    {
        protected CustomQuery(string name, string type, int sort, CustomeQueryGroupExpression queryCriteria, AccountInfo accountInfo, Operation operation)
        {
            Name = name;
            Type = type;
            AccountInfo = accountInfo;
            Sort = sort;
            QueryCriteria = queryCriteria;

            CreateOperation = operation.CloneOperation();
            LastModifyOperation = operation.CloneOperation();
        }

        /// <summary>
        /// 自定义查询名称（筛选组名称）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 自定义查询的所属类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 所属账户
        /// </summary>
        public AccountInfo AccountInfo { get; private set; }

        /// <summary>
        /// 自定义查询的条件
        /// </summary>
        public CustomeQueryGroupExpression QueryCriteria { get; set; }

        /// <summary>
        /// 创建时操作信息
        /// </summary>
        public Operation CreateOperation { get; protected set; }

        /// <summary>
        /// 最后更新操作信息
        /// </summary>
        public Operation LastModifyOperation { get; protected set; }
        
        public void ChangeSort(int sort)
        {
             Sort = sort;
        }

        /// <summary>
        /// 修改名字
        /// </summary>
        /// <param name="name"></param>
        public void ChangeName(string name)
        {
            this.Name = name;
        }
        public void ChangeQueryCriteria(CustomeQueryGroupExpression queryCriteria)
        {
            QueryCriteria = queryCriteria;
        }

        /// <summary>
        /// 更新最新操作者信息
        /// </summary>
        /// <param name="operation">操作信息</param>
        public void UpdateLastModifyOperation(Operation operation)
        {
            LastModifyOperation = operation.CloneOperation();
        }
    }
}
