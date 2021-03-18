// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.ApiResourceAggregate;
namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static IS4Models.ApiResource ToModel(this DomainModels.ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<IS4Models.ApiResource >(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DomainModels.ApiResource ToEntity(this IS4Models.ApiResource model)
        {
            return model == null ? null : Mapper.Map<DomainModels.ApiResource>(model);
        }
    }
}