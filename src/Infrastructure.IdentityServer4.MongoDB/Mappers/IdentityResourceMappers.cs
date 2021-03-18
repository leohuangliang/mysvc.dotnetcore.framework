// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.IdentityResourceAggregate;
namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for identity resources.
    /// </summary>
    public static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IS4Models.IdentityResource ToModel(this DomainModels.IdentityResource entity)
        {
            return entity == null ? null : Mapper.Map<IS4Models.IdentityResource>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DomainModels.IdentityResource ToEntity(this IS4Models.IdentityResource model)
        {
            return model == null ? null : Mapper.Map<DomainModels.IdentityResource>(model);
        }
    }
}