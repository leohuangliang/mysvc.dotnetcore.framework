namespace Capmarvel.Framework.Applications.Common.Constants
{
    /// <summary>
    /// 自定义查询表达式的类型
    /// </summary>
    public static class CustomeQueryExpressionType
    {
        /// <summary>
        /// 组表达式
        /// </summary>
        public const string GROUP = "Group";

        /// <summary>
        /// 字符串数据单个匹配
        /// </summary>
        public const string STRING_MATCH = "StringMatch";

        /// <summary>
        /// 字符串数据多值匹配
        /// </summary>
        public const string STRING_MULTI_MATCH = "StringMultiMatch";

        /// <summary>
        /// 整型数据单个匹配
        /// </summary>
        public const string INT_MATCH = "IntMatch";

        /// <summary>
        /// 整型数据范围匹配
        /// </summary>
        public const string INT_RANGE = "IntRange";

        /// <summary>
        /// 整型数据多值匹配
        /// </summary>
        public const string INT_MULTI_MATCH = "IntMultiMatch";

        /// <summary>
        /// 数字数据单个匹配
        /// </summary>
        public const string DECIMAL_MATCH = "DecimalMatch";

        /// <summary>
        /// 数字数据范围匹配
        /// </summary>
        public const string DECIMAL_RANGE = "DecimalRange";

        /// <summary>
        /// 数字数据多值匹配
        /// </summary>
        public const string DECIMAL_MULTI_MATCH = "DecimalMultiMatch";

        /// <summary>
        /// 日期时间数据单个匹配
        /// </summary>
        public const string DATETIME_MATCH = "DatetimeMatch";

        /// <summary>
        /// 日期时间数据范围匹配
        /// </summary>
        public const string DATETIME_RANGE = "DatetimeRange";

        /// <summary>
        ///  日期时间数据多值匹配
        /// </summary>
        public const string DATETIME_MULTI_MATCH = "DatetimeMultiMatch";

        /// <summary>
        /// 布尔值数据单个匹配
        /// </summary>
        public const string BOOL_MATCH = "BoolMatch";
    }
}
