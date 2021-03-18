using AutoMapper;

using System.Collections.Generic;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.IdentityResourceAggregate;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// AutoMapper configuration for identity resource
    /// Between model and entity
    /// </summary>
    public class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            CreateMap<DomainModels.IdentityResourceProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap< DomainModels.IdentityResource, IS4Models.IdentityResource>(MemberList.Destination)
                .ConstructUsing(src => new IS4Models.IdentityResource())
                .ReverseMap();

            CreateMap<DomainModels.IdentityResourceClaim, string>()
                .ConstructUsing(x => x.Type)
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
        }
    }
}
