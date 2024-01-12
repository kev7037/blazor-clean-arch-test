using Mc2.CrudTest.Core.Domain.Customers.Events;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.C_DomainEvent.Events
{
    public class DummyCustomerUpdatedHandler : INotificationHandler<CustomerUpdated>
    {
        public Task Handle(CustomerUpdated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Customer Updated Event Handled" + notification.Id + " - " + notification.FirstName);
            return Task.CompletedTask;
        }
    }
}
