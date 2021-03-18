using AutoMapper;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate;
namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// AutoMapper Config for PersistedGrant
    /// Between Model and Entity
    /// <seealso cref="https://github.com/AutoMapper/AutoMapper/wiki/Configuration">
    /// </seealso>
    /// </summary>
    public class PersistedGrantMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="PersistedGrantMapperProfile">
        /// </see>
        /// </summary>
        public PersistedGrantMapperProfile()
        {
            CreateMap<DomainModels.PersistedGrant, IS4Models.PersistedGrant>(MemberList.Destination)
                .ReverseMap();
        }
    }
}
