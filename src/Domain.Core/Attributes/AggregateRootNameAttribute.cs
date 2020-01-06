using System;

namespace MySvc.DotNetCore.Framework.Domain.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AggregateRootNameAttribute : Attribute
    {
        /// <summary>
        /// The name of the collection in which your documents are stored.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
		public AggregateRootNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}