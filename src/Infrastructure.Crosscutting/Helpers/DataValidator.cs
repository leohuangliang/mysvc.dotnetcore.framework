using System;
using System.Text.RegularExpressions;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 公共数据校验器
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// 校验字符串是否是一个EMAIL地址
        /// </summary>
        /// <param name="str">需要校验的字符串</param>
        /// <returns>是否是EMAIL地址</returns>
        public static bool IsEmail(string str)
        {
            return !string.IsNullOrEmpty(str) && str.IsMatch(RegexConst.EMAIL);
        }

        /// <summary>
        /// 判断输入的字符串只包含汉字
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsChineseCh(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.CHINESE_WORD);
        }

        /// <summary>
        /// 判断输入的字符串字包含英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglisCh(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.ENGLISH_WORD);
        }

        /// <summary>
        /// 判断输入的字符串是否符合一个电话号码的格式
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsPhone(string input)
        {            
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.PHONE);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsMobilePhone(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.MOBILE_PHONE);
        }
        
        /// <summary>
        /// 判断输入的字符串只包含数字
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.NUMBER);
        }

        /// <summary>
        /// 判断输入的字符串是否符合一个非负整数
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsNotNagtive(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.NOT_NAGTIVE);
        }

        /// <summary>
        /// 判断输入的字符串是否符合一个正整数
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsPositiveInteger(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.POSITIVE_INTEGER);
        }
         
        /// <summary>
        /// 判断输入的字符串是否只包含数字和英文字母
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsNumAndEnCh(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.NUMBER_AND_ENGLISH);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个超链接
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsURL(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.URL);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个符合IPV4的IP地址
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static bool IsIPv4(string input)
        {
            return !string.IsNullOrEmpty(input) && input.IsMatch(RegexConst.IPV4);
        }
        
        /// <summary>
        /// 判断输入的字符串是否是合法的IPV6 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPV6(string input)
        {
            /* *******************************************************************
            * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
            * 2、判断输入的IPV6字符串中是否有“::”。
            * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
            * 4、如果有“::” ，判断"::"是否止出现一次
            * 5、如果出现一次以上 返回false
            * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
            * ******************************************************************/
            string pattern = "";
            string temp = input;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = input.GetStringCount("::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";
                return input.IsMatch(pattern);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                return input.IsMatch(pattern);
            }
        }
    }
}