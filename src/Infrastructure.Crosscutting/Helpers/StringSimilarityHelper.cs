using System;
using System.Linq;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 比较字符串相识度辅助器
    /// </summary>
    public static class StringSimilarityHelper
    {
        /// <summary>
        /// 计算两个字符串的编辑距离， 基于Levenshtein Distance算法实现
        /// 
        /// 编辑距离（Edit Distance），又称Levenshtein距离，是指两个字串之间，由一个转成另一个所需的最少编辑操作次数,
        /// 许可的编辑操作包括将一个字符替换成另一个字符，插入一个字符，删除一个字符。
        /// 一般来说，编辑距离越小，两个串的相似度越大。
        /// </summary>
        /// <param name="word1">字符串1</param>
        /// <param name="word2">字符串2</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>编辑距离</returns>
        public static int CalcEditDistance(string word1, string word2, bool ignoreCase = false)
        {
            if (word1 == null || word2 == null)
            {
                throw new ArgumentNullException();
            }

            string s = word1;
            string t = word2;

            if (ignoreCase)
            {
                s = word1.ToLower();
                t = word2.ToLower();
            }

            #region

            int n = s.Length;
            int m = t.Length;

            var d = new int[n + 1, m + 1];

            //step1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            //step2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }
            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            //step3
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];

            #endregion
        }

        /// <summary>
        ///  计算最长公共子序列（Longest Common Subsequence）
        /// 
        ///  一个序列，如果是两个或多个已知序列的子序列，且是所有子序列中最长的，则为最长公共子序列。
        /// </summary>
        public static int CalcLongestCommonSubsequence(string source, string target, bool ignoreCase = true)
        {
            if (source == null || target == null || source.Length == 0 || target.Length == 0)
                return 0;

            if (ignoreCase)
            {
                source = source.ToLower();
                target = source.ToLower();
            }

            int len = Math.Max(target.Length, source.Length);
            int[,] subsequence = new int[len + 1, len + 1];
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < target.Length; j++)
                {
                    if (source[i].Equals(target[j]))
                        subsequence[i + 1, j + 1] = subsequence[i, j] + 1;
                    else
                        subsequence[i + 1, j + 1] = 0;
                }
            }
            int maxSubquenceLenght = (from sq in subsequence.Cast<int>() select sq).Max<int>();
            return maxSubquenceLenght;
        }

        /// <summary>
        /// 计算两字符串的相似度，结合Levenshtein Distance + LCS算法
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>字符串的相似度（越大越相识）</returns>
        public static float CalcStringSimilarity(string source, string target, bool ignoreCase = true) {
            var ld = CalcEditDistance(source, target, ignoreCase);
            var lcs = CalcLongestCommonSubsequence(source, target);
            return ((float)lcs) / (ld + lcs); ;
        }
 
    }
}