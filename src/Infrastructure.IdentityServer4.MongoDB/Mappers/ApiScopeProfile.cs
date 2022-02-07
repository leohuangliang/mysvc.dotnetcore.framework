using AutoMapper;
using System.Collections.Generic;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.Framework.IS4.Domain.ApiScopeAggregate;

namespace MySvc.Framework.IS4.MongoDB.Mappers
{
    public class ApiScopeProfile : Profile
    {
        public ApiScopeProfile()
        {

            CreateMap<DomainModels.ApiScopeProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<DomainModels.ApiScopeClaim, string>()
                .ConstructUsing(x => x.Type)
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));

            CreateMap<DomainModels.ApiScope, IS4Models.ApiScope>(MemberList.Destination)
                .ConstructUsing(src => new IS4Models.ApiScope())
                .ForMember(x => x.Properties, opts => opts.MapFrom(x => x.Properties))
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(x => x.UserClaims))
                .ReverseMap();
        }
    }
}
