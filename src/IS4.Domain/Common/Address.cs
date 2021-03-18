using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace MySvc.DotNetCore.Framework.IS4.Domain.Common
{
    public class Address : ValueObject<Address>
    {
        public Address(string country, AddressArea province, AddressArea city, AddressArea district, string addressLine)
        {
            this.Country = this.Country;
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
        public AddressArea Province { get; private set; }

        /// <summary>
        /// 市
        /// </summary>
        public AddressArea City { get; private set; }

        /// <summary>
        /// 区/县
        /// </summary>
        public AddressArea District { get; private set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string AddressLine { get; private set; }


        public void SetCountry(string country)
        {
            this.Country = country;
        }

        public void SetProvince(string provinceName,string provinceCode)
        {
            this.Province = new AddressArea(provinceName, provinceCode);
        }

        public void SetCity(string cityName,string cityCode)
        {
            this.City = new AddressArea(cityName, cityCode);
        }

        public void SetDistrict(string districtName, string districtCode)
        {
            this.District = new AddressArea(districtName, districtCode);
        }

        public void SetAddressLine(string addressLine)
        {
            this.AddressLine = addressLine;
        }
    }

    public class AddressArea : ValueObject<AddressArea>
    {
        public AddressArea(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public string Name { get; private set; }

        public string Code { get; private set; }
    }
}
