using System.Text.RegularExpressions;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 一些常用的正则表达式
    /// </summary>
    public static class RegexConst
    {
        /// <summary>
        /// Email的正则表达式
        /// </summary>
        public static readonly Regex NO_ASCII_CHAR = new Regex(@"[^\u0000-\u007f]", RegexOptions.Compiled);

        /// <summary>
        /// Email的正则表达式
        /// </summary>
        //public static readonly Regex EMAIL = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Compiled);
        public static readonly Regex EMAIL = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);

        /// <summary>
        /// 只包含中文的正则表达式
        /// </summary>
        public static readonly Regex CHINESE_WORD = new Regex(@"[\u4e00-\u9fa5]+", RegexOptions.Compiled);

        /// <summary>
        /// 只包含英文的正则表达式
        /// </summary>
        public static readonly Regex ENGLISH_WORD = new Regex(@"^[A-Za-z]+$", RegexOptions.Compiled);

        /// <summary>
        /// 只包含数字和英文字母
        /// </summary>
        /// <returns></returns>
        public static readonly Regex NUMBER_AND_ENGLISH = new Regex(@"^[A-Za-z0-9]+$", RegexOptions.Compiled);

        /// <summary>
        /// 电话号码的正则表达式
        /// Note:
        ///     匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，
        ///     也可以不用，区号与本地号间可以用连字号或空格间隔，
        ///     也可以没有间隔
        ///     \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}
        /// </summary>
        public static readonly Regex PHONE = new Regex(@"^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$", RegexOptions.Compiled);

        /// <summary>
        /// 手机号码的正则表达式
        /// </summary>
        public static readonly Regex MOBILE_PHONE = new Regex(@"^13\\d{9}$", RegexOptions.Compiled);
        
        /// <summary>
        /// 只包含数字的正则表达式（匹配整数和浮点数）
        /// </summary>
        public static readonly Regex NUMBER = new Regex(@"^-?\\d+$|^(-?\\d+)(\\.\\d+)?$", RegexOptions.Compiled);

        /// <summary>
        /// 非负整数(Nonnegative integer)的正则表达式
        /// </summary>
        public static readonly Regex NOT_NAGTIVE = new Regex(@"^\d+$", RegexOptions.Compiled);

        /// <summary>
        /// 正整数（positive integer）的正则表达式
        /// </summary>
        public static readonly Regex POSITIVE_INTEGER = new Regex(@"^[0-9]*[1-9][0-9]*$", RegexOptions.Compiled);

        /// <summary>
        /// URL的正则表达式
        /// </summary>
        public static readonly Regex URL = new Regex(@"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$", RegexOptions.Compiled);

        /// <summary>
        /// IPv4的正则表达式
        /// </summary>
        public static readonly Regex IPV4 = new Regex(@"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}$", RegexOptions.Compiled);

        /// <summary>
        /// 表示字符串中换行的正则表达式
        /// </summary>
        public static readonly Regex LINE_FEED = new Regex(@"\r?\n", RegexOptions.Compiled);

        /// <summary>
        /// 匹配连续多个空/制表符的正则表达式
        /// </summary>
        public static readonly Regex SPACE_MORE_THAN_ONE_CHAR = new Regex(@"\s\s+", RegexOptions.Compiled);
    }
}