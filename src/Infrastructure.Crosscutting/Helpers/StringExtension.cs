using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// String common extesion methods
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 符号过滤
        /// </summary>
        public static readonly string[] SYMBOL_FILTERS = {" ", ",", ".", "&", "%", "#", "@", "!", "*", "(", ")"};

        /// <summary>
        /// 表示空一个空格的字符串
        /// </summary>
        public const string ONE_SPACE_CHAR = " ";

        /// <summary>
        /// Formats a string. Similar to String.Format()
        /// </summary>
        /// <param name="target">string to format</param>
        /// <param name="args">Arguments to replace in the string.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotNullOrEmpty(target, "target");
            return string.Format(target, args);
        }

        /// <summary>
        /// Verifies whether the string is a valid e-mail address.
        /// </summary>
        /// <param name="target">String containing a value that could be an e-mail address.</param>
        /// <returns>True if the value is a valid e-mail. Otherwise, false.</returns>
        public static bool IsEmail(this string target)
        {
            return DataValidator.IsEmail(target);
        }

        /// <summary>
        /// Valid a string is not null or white space. Similar to string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>True if the value through verification, Otherwise, false.</returns>
        public static bool IsNullOrBlank(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Valid a string is not null or white space
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>True if the value through verification, Otherwise, false.</returns>
        public static bool NotNullOrBlank(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool ContainsNoASCIIChar(this string str)
        {
            return RegexConst.NO_ASCII_CHAR.Match(str).Success;
        }
        
        /// <summary>
        /// 串接字符
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="split">连接符</param>
        /// <param name="value">需要串接的字符</param>
        /// <returns>串接后的字符</returns>
        public static string JoinString(this string str, string split, string value)
        {
            if (value.IsNullOrBlank()) return str;
            if (str.IsNullOrBlank())
            {
                str = value;
                return str;
            }
            str += split + value;
            return str;
        }

        /// <summary>
        ///  正则替换
        /// </summary>
        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }

        /// <summary>
        /// 调用Regex中IsMatch函数实现一般的正则表达式匹配
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式(字符串形式表示)。</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsMatch(this string str, string pattern)
        {
            if (str.IsNullOrBlank())
            {
                return false;
            }

            var regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

         /// <summary>
        /// 调用Regex中IsMatch函数实现一般的正则表达式匹配
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="regex">要匹配的正则表达式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsMatch(this string str, Regex regex)
        {
            if (str.IsNullOrBlank())
            {
                return false;
            }

            return regex.IsMatch(str);
        }

        /// <summary>
        /// 检查是否包含指定的子串，不区分大小写
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="subString">子字符串</param>
        /// <returns>检查结果</returns>
        public static bool ContainsIgnoreCase(this string str, string subString)
        {
            return str.ToUpper().Contains(subString.ToUpper());
        }

        /// <summary>
        /// 判断字符串compare 在 input字符串中出现的次数
        /// 
        /// input.GetStringCount("::"); 返回::在字符串出现的次数
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="compare">用于比较的字符串</param>
        /// <returns>字符串compare 在 input字符串中出现的次数</returns>
        public  static int GetStringCount(this string input, string compare) 
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 从输入字符串中的第一个字符开始，用替换字符串替换指定的正则表达式模式的所有匹配项。
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="replacement">用于替换的字符串</param>
        /// <returns>返回被替换后的结果</returns>
        public static string Replace(this string input, string pattern, string replacement)
        {
            Regex regex = new Regex(pattern);
            return regex.Replace(input, replacement);
        }

        /// <summary>
        /// 在由正则表达式模式定义的位置拆分输入字符串。
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <returns>拆分的字符串数组</returns>
        public static string[] Split(this string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }

        /// <summary>
        /// 计算字符串的字符长度，一个汉字字符将被计算为两个字符
        /// </summary>
        /// <param name="input">需要计算的字符串</param>
        /// <returns>返回字符串的长度</returns>
        public static int GetCount(this string input)
        {
            return Regex.Replace(input, @"[\u4e00-\u9fa5/g]", "aa").Length;
        }
        
        /// <summary>
        /// 判断字符串是否相等（忽略大小写）
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="comparedStr">比较的字符串</param>
        public static bool EqualsIgnoreCase(this string str, string comparedStr)
        {
            return str.Equals(comparedStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否以指定字符串开头（忽略大小写）
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="comparedStr">比较的字符串</param>
        public static bool StartsWithIgnoreCase(this string str, string comparedStr)
        {
            return str.StartsWith(comparedStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 消除标点符号(" ", ",", ".", "&", "%", "#", "@", "!", "*", "(", ")")
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>消除标点符号后的字符串</returns>
        public static string CleanSymbol(this string str)
        {
            return SYMBOL_FILTERS.Aggregate(str, (current, s) => current.Replace(s, ""));
        }

        /// <summary>
        /// 清除常见转义字符(\r \n \t)，多个空格变成一个
        /// </summary>
        public static string ClearNormal(this string str)
        {
            return RegexConst.SPACE_MORE_THAN_ONE_CHAR.Replace(
                RegexConst.LINE_FEED.Replace(str.Trim(), 
                ONE_SPACE_CHAR),
                ONE_SPACE_CHAR);
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256(this string input)
        {
            if (input.IsNullOrBlank()) return string.Empty;

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }

        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static byte[] Sha256(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha512(this string input)
        {
            if (input.IsNullOrBlank()) return string.Empty;

            using (var sha = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}