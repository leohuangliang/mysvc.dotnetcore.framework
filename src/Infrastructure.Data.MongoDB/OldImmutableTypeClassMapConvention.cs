﻿/* Copyright 2016-present MongoDB Inc.
*
* This is the original file ImmutableTypeClassMapConvention from commit da9c1d2e
* because in a future version behavior was changed and causes regressions.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
* 
*/

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Linq;
using System.Reflection;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    /// <summary>
    /// Maps a fully immutable type. This will include anonymous types.
    /// </summary>
    public class OldImmutableTypeClassMapConvention : ConventionBase, IClassMapConvention
    {
        /// <inheritdoc />
        public void Apply(BsonClassMap classMap)
        {
            var typeInfo = classMap.ClassType.GetTypeInfo();
            if (typeInfo.IsAbstract)
            {
                return;
            }

            if (typeInfo.GetConstructor(Type.EmptyTypes) != null)
            {
                return;
            }

            var properties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Any(p => p.CanWrite))
            {
                return; // a type that has any writable properties is not immutable
            }

            var anyConstructorsWereMapped = false;
            foreach (var ctor in typeInfo.GetConstructors())
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length != properties.Length)
                {
                    continue; // only consider constructors that have sufficient parameters to initialize all properties
                }

                var matches = parameters
                    .GroupJoin(properties,
                        parameter => parameter.Name,
                        property => property.Name,
                        (parameter, props) => new { Parameter = parameter, Properties = props },
                        StringComparer.OrdinalIgnoreCase);

                if (matches.Any(m => m.Properties.Count() != 1))
                {
                    continue;
                }

                classMap.MapConstructor(ctor);

                anyConstructorsWereMapped = true;
            }

            if (anyConstructorsWereMapped)
            {
                // if any constructors were mapped by this convention then map all the properties also
                foreach (var property in properties)
                {
                    var memberMap = classMap.MapMember(property);
                    if (classMap.IsAnonymous)
                    {
                        var defaultValue = memberMap.DefaultValue;
                        memberMap.SetDefaultValue(defaultValue);
                    }
                }
            }
        }
    }
}
