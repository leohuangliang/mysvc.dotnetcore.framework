using System;

namespace Capmarvel.Framework.Applications.Common.Models
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}