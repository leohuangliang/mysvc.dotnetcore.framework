using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System.Collections.Generic;
using System.Linq;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    public static class ConventionRegistryHelper
    {
        public static void ReplaceDefaultConventionPack()
        {
            ConventionRegistry.Remove("__defaults__");
            var pack = new ConventionPack();

            var defaultConventions = DefaultJarvisConventionPack.Instance.Conventions;

            pack.AddRange(defaultConventions.Except(
                defaultConventions.OfType<ImmutableTypeClassMapConvention>()
            ));

            
            ConventionRegistry.Register(
                "__defaults__",
                pack,
                t => true);
            ConventionRegistry.Register("__defaults__", pack, t => true);
        }

        public class DefaultJarvisConventionPack : IConventionPack
        {
            // private static fields
            private static readonly IConventionPack __defaultConventionPack = new DefaultJarvisConventionPack();

            // private fields
            private readonly IEnumerable<IConvention> _conventions;

            // constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultConventionPack" /> class.
            /// </summary>
            private DefaultJarvisConventionPack()
            {
                _conventions = new List<IConvention>
                {
                    new ReadWriteMemberFinderConvention(),
                    new NamedIdMemberConvention(new [] { "Id", "id", "_id" }),
                    new NamedExtraElementsMemberConvention(new [] { "ExtraElements" }),
                    new IgnoreExtraElementsConvention(true),
                    //new OldImmutableTypeClassMapConvention(),
                    //new ImmutableTypeClassMapConvention(),
                    new EnumRepresentationConvention(BsonType.String),
                    new NamedParameterCreatorMapConvention(),
                    new StringObjectIdIdGeneratorConvention(), // should be before LookupIdGeneratorConvention
                    new LookupIdGeneratorConvention()
                };
            }

            // public static properties
            /// <summary>
            /// Gets the instance.
            /// </summary>
            public static IConventionPack Instance
            {
                get { return __defaultConventionPack; }
            }

            // public properties
            /// <summary>
            /// Gets the conventions.
            /// </summary>
            public IEnumerable<IConvention> Conventions
            {
                get { return _conventions; }
            }
        }
    }

    
}
