using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core.DomainEvents;
using Xunit.Abstractions;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class EmployeeDismmisionDomainEventHandler : IDomainEventHandler<EmployeeDismmisionDomainEvent>
    {
        private readonly ITestOutputHelper _output;
        public EmployeeDismmisionDomainEventHandler(ITestOutputHelper output)
        {
            _output = output;
        }

        public Task Handle(EmployeeDismmisionDomainEvent notification, CancellationToken cancellationToken)
        {
            _output.WriteLine("Employee Dimission : Id = [{0}]  Name:[{1}]  EmployeeNo:[{2}] ", 
                notification.Employee.Id,notification.Employee.Name, notification.Employee.EmployeeNo);
            return Task.CompletedTask;
        }
    }
}
