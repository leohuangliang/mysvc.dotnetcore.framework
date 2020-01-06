using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;


namespace Infrastructure.Data.MongoDB.Tests
{
    [AggregateRootName("Person")]
    public class Person : AggregateRoot
    {
        public string Name { get; set; }
    }
}