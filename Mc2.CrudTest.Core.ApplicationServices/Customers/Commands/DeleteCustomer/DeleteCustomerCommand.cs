using MediatR;

namespace Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<long>
    {
        public long Id { get; set; }

    }
}
