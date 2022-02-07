using MySvc.Framework.Domain.Core.Impl;

namespace Sample.Order.Domain.Common.Models
{
    /// <summary>
    /// 表示地址信息
    /// </summary>
    public class Address : ValueObject<Address>
    {
        private Address()
        {
        }

        public Address(string country, string city,
            string provicne, string postCode, string street1, string street2,
            string street3)
        {
            Country = country != null ? country.Trim() : string.Empty;
            City = city != null ? city.Trim() : string.Empty;
            Province = provicne != null ? provicne.Trim() : string.Empty;
            Postcode = postCode != null ? postCode.Trim() : string.Empty;
            Street1 = street1 != null ? street1.Trim() : string.Empty;
            Street2 = street2 != null ? street2.Trim() : string.Empty;
            Street3 = street3 != null ? street3.Trim() : string.Empty;
        }

        /// <summary>
        /// 街道1
        /// </summary>
        public string Street1 { get; private set; }

        /// <summary>
        /// 街道2
        /// </summary>
        public string Street2 { get; private set; }

        /// <summary>
        /// 街道3
        /// </summary>
        public string Street3 { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string Postcode { get; private set; }
    }
}
