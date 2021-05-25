using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;


namespace Infrastructure.Data.MongoDB.Tests
{
    [AggregateRootName("Person")]
    public abstract class  Person : AggregateRoot
    {
        protected Person(string name)
        {
            this.Name = name;
        }
        public string Name { get; protected set; }

        public string Name1 { get; set; }
    }
}