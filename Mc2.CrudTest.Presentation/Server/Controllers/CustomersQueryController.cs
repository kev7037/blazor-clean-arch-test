using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetCustomerById;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersQueryController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mediator _mediator;

        public CustomersQueryController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mediator = new Mediator(_serviceProvider);
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            List<CustomerDto> customers = await _mediator.Send(new GetAllCustomersQuery());
            Console.WriteLine("get all: " + customers.FirstOrDefault()?.FirstName);
            return customers;
        }

        [HttpGet]
        [Route("GetAllCustomers/{Id}")]
        public async Task<CustomerDto?> GetCustomerById(long id)
        {
            CustomerDto? customer = await _mediator.Send(new GetCustomerByIdQuery() { Id = id });
            Console.WriteLine("get all: " + customer?.FirstName);
            return customer;
        }


    }
}
