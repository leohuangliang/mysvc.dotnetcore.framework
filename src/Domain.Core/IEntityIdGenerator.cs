using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    /// <summary>
    /// 实体ID生成器
    /// </summary>
    public interface IEntityIdGenerator
    {
        /// <summary>
        /// 生成新的ID
        /// </summary>
        /// <returns></returns>
        string GenerateId();
    }
}
