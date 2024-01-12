using Mc2.CrudTest.Core.Domain.Customers.Events;
using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.C_DomainEvent.Events
{
    public class DummyCustomerDeletedHandler : INotificationHandler<CustomerDeleted>
    {
        public Task Handle(CustomerDeleted notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Customer Deleted Event Handled" + notification.Id);
            return Task.CompletedTask;
        }
    }
}
