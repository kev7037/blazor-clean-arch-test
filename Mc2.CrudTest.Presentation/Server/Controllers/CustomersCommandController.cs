using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersCommandController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Mediator mediator;

        public CustomersCommandController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            mediator = new Mediator(serviceProvider);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var mediator = new Mediator(serviceProvider);
            var customer_id = await mediator.Send(command);
            return CreatedAtAction("GetById", "CustomersCommandController", new {id = customer_id, command});
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
            var customer_id = await mediator.Send(command);
            return Ok(customer_id);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteCustomerCommand command)
        {
            var customer_id = await mediator.Send(command);
            return Ok(customer_id);
        }


    }
}