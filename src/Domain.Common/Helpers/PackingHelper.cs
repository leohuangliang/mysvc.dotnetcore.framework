using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.Models;

namespace Capmarvel.Framework.Domain.Common.Helpers
{
    /// <summary>
    /// 尺寸帮助类
    /// </summary>
    public static class PackingHelper
    {
        /// <summary>
        /// 尺寸正则表达式
        /// </summary>
        public static readonly Regex SIZE_REGEX =
            new Regex(@"^[0-9]+(?:\.[0-9]+)?\*[0-9]+(?:\.[0-9]+)?\*[0-9]+(?:\.[0-9]+)?$");

        /// <summary>
        /// 空尺寸（单位为CM）
        /// </summary>
        public static Packing NullSizeInCM
        {
            get { return new Packing(0, 0, 0, LengthUnit.CM); }
        }

        /// <summary>
        /// 将字符串表示的尺寸转换为Packing对象
        /// </summary>
        /// <param name="packingStr">字符串表示的尺寸</param>
        /// <param name="lengthUnit">长度单位</param>
        /// <returns>Packing对象</returns>
        public static Packing GetPacking(string packingStr, string lengthUnit = LengthUnit.CM)
        {
            if (!string.IsNullOrWhiteSpace(packingStr))
            {
                packingStr = packingStr.Trim();

                //检查尺寸字符串是否符合规格
                if (!IsValidPackingString(packingStr))
                {
                    throw new ArgumentException(string.Format("\"{0}\" 不是符合规范的体积数据，请检查确认", packingStr));
                }

                var edges = packingStr.Split('*').Select(decimal.Parse).ToArray();

                edges = edges.OrderByDescending(x => x).ToArray();

                return new Packing(edges[0], edges[1], edges[2], lengthUnit);
            }

            return null;
        }

        /// <summary>
        /// 判断字符串是否格式正确
        /// </summary>
        /// <param name="packingStr">字符串格式的尺寸</param>
        /// <returns>是否是有效的尺寸</returns>
        public static bool IsValidPackingString(string packingStr)
        {
            return SIZE_REGEX.IsMatch(packingStr);
        }

        /// <summary>
        /// 合并尺寸
        /// </summary>
        /// <param name="sizeList">尺寸集合</param>
        /// <param name="lengthUnit">目标单位，为空则取尺寸集合中第一个单位</param>
        /// <returns>合并后的尺寸</returns>
        public static Packing Combine(IEnumerable<Packing> sizeList, string lengthUnit = "")
        {
            if (sizeList == null || !sizeList.Any())
            {
                throw new ArgumentException("sizeList can not be empty");
            }

            lengthUnit = string.IsNullOrWhiteSpace(lengthUnit) ? sizeList.FirstOrDefault().LengthUnit : lengthUnit;

            foreach (var packing in sizeList)
            {
                packing.ConvertLengthUnit(lengthUnit);
            }

            //排序尺寸
            sizeList = sizeList.OrderBy(x => x.Length).ThenBy(x => x.Width).ThenBy(x => x.Height).ToList();

            Packing t = null;

            foreach (Packing s in sizeList)
            {
                if (t == null)
                {
                    t = s;
                }
                else
                {
                    t = new Packing(s.Length >= t.Length ? s.Length : t.Length, 
                        s.Width >= t.Width ? s.Width : t.Width,
                        s.Height + t.Height,
                        lengthUnit);
                }
            }

            return t;
        }
    }
}
