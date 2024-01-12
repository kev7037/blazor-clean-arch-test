using Mc2.CrudTest.Core.Domain.Customers.Events;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.C_DomainEvent.Events
{
    public class DummyCustomerCreatedHandler : INotificationHandler<CustomerCreated>
    {
        public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Customer Created Event Handled" + notification.Id + " - " + notification.FirstName);
            return Task.CompletedTask;
        }
    }
}
