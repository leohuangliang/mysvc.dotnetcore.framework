using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;


namespace Infrastructure.Data.MongoDB.Tests
{
    [AggregateRootName("Person")]
    public class Employee : Person
    {
        public string EmployeeNo { set; get; }

        public  bool IsDimission { get; private set; }

        public void Dimission()
        {
            this.IsDimission = true;
            base.AddDomainEvent(new EmployeeDismmisionDomainEvent(this));
        }
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