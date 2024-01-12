using Mc2.CrudTest.Presentation.Shared.HelperClasses;

namespace Mc2.CrudTest.Core.Domain.Customers.Events
{
    public class CustomerDeleted : IDomainEvent
    {
        public long Id { get; set; }
    }
}
