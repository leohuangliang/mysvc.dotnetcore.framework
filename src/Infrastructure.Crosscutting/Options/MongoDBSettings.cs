using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.Framework.Infrastructure.Crosscutting.Options
{
    /// <summary>
    /// MongoDB的配置项
    /// </summary>
    public class MongoDBSettings
    {
        /// <summary>
        /// MongoDB连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 指定的MongoDB数据库
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 是否禁用数据行的并发控制
        /// </summary>
        public bool DisableConcurrencyControl { get; set; }
    }
}
