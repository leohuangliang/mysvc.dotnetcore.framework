using Sample.Order.Application.Common.Models;

namespace Sample.Order.Application.Extensions
{
    /// <summary>
    /// DTO 转Domain的通用方法
    /// </summary>
    public static class DomainTransformExtension
    {
        /// <summary>
        /// DTO的Address转换为Domian的Address
        /// </summary>
        public static Domain.Common.Models.Address ToDomain(this Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new Domain.Common.Models.Address(address.Country, address.City, address.Province, address.Postcode,
                address.Street1, address.Street2, address.Street3);
        }

    }
}
