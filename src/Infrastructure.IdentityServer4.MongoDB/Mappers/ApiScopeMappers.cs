// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.ApiScopeAggregate;
namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for scopes.
    /// </summary>
    public static class ApiScopeMappers
    {
        static ApiScopeMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiScopeProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IS4Models.ApiScope ToModel(this DomainModels.ApiScope entity)
        {
            return entity == null ? null : Mapper.Map<IS4Models.ApiScope>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DomainModels.ApiScope ToEntity(this global::IdentityServer4.Models.ApiScope model)
        {
            return model == null ? null : Mapper.Map<DomainModels.ApiScope>(model);
        }
    }
}