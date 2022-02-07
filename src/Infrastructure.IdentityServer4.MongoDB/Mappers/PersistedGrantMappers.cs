// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.Framework.IS4.Domain.PersistedGrantAggregate;
namespace MySvc.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for persisted grants.
    /// </summary>
    public static class PersistedGrantMappers
    {
        static PersistedGrantMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IS4Models.PersistedGrant ToModel(this DomainModels.PersistedGrant entity)
        {
            return entity == null ? null : Mapper.Map<IS4Models.PersistedGrant>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DomainModels.PersistedGrant ToEntity(this IS4Models.PersistedGrant model)
        {
            return model == null ? null : Mapper.Map<DomainModels.PersistedGrant>(model);
        }

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        public static void UpdateEntity(this IS4Models.PersistedGrant model, DomainModels.PersistedGrant entity)
        {
            Mapper.Map(model, entity);
        }
    }
}