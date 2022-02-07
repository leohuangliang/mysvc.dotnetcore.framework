using MongoDB.Bson.Serialization.Conventions;
using System.Collections.Generic;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    public static class ConventionRegistryHelper
    {
        public static void ReplaceDefaultConventionPack()
        {
            ConventionRegistry.Remove("__defaults__");
            ConventionRegistry.Register("__defaults__", DefaultJarvisConventionPack.Instance, t => true);
        }
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
                new IgnoreExtraElementsConvention(false),
                new OldImmutableTypeClassMapConvention(),
                //new ImmutableTypeClassMapConvention(),
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
