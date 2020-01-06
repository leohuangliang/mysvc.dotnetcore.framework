using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
namespace Domain.Core.Tests
{
    public class Address: ValueObject<Address>
    {
        public Address(string country, string province, string city, string district, string addressLine)
        {
            this.Country = Country;
            this.Province = province;
            this.City = city;
            this.District = district;
            this.AddressLine = addressLine;
        }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; private set; } = "CN";

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区/县
        /// </summary>
        public string District { get; private set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string AddressLine { get; private set; }
    }
}