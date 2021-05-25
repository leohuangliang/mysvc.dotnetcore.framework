using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;


namespace Infrastructure.Data.MongoDB.Tests
{
    [AggregateRootName("Person")]
    public class Employee : Person
    {
        public Employee(string name) : base(name)
        {
            this.NameInfo = new NameInfo(name);

        }
        public string EmployeeNo { set; get; }

        public  bool IsDimission { get; private set; }

        public NameInfo NameInfo { get; private set; }

        public void Dimission()
        {
            this.IsDimission = true;
            base.AddDomainEvent(new EmployeeDismmisionDomainEvent(this));
        }
    }

    public class NameInfo : ValueObject<NameInfo>
    {
        public NameInfo(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set;}

    }
 
    public class EmployeeDismmisionDomainEvent : IDomainEvent
    {
        public Employee Employee { get; private set; }

        public EmployeeDismmisionDomainEvent(Employee employee)
        {
            this.Employee = employee;
        }

    }

    
}