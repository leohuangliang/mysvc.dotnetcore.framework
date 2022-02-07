// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IS4Models=IdentityServer4.Models;
using DomainModels = MySvc.Framework.IS4.Domain.ClientAggregate;

namespace MySvc.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for clients.
    /// </summary>
    public static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IS4Models.Client ToModel(this DomainModels.Client entity)
        {
            return Mapper.Map<IS4Models.Client>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DomainModels.Client ToEntity(this IS4Models.Client model)
        {
            return Mapper.Map<DomainModels.Client>(model);
        }
    }
}