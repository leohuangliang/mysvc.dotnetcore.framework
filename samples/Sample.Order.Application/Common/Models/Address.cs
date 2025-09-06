namespace Sample.Order.Application.Common.Models
{
    /// <summary>
    /// 表示地址信息
    /// </summary>
    public class Address
    {
        /// <summary>
        /// 街道1
        /// </summary>
        public required string Street1 { get; set; }

        /// <summary>
        /// 街道2
        /// </summary>
        public required string Street2 { get; set; }

        /// <summary>
        /// 街道3
        /// </summary>
        public required string Street3 { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public required string Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public required string Country { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public required string Postcode { get; set; }
    }
}
