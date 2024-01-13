using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Core.ApplicationServices.Customers.Commands.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersCommandController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mediator _mediator;

        public CustomersCommandController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mediator = new Mediator(_serviceProvider);
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            long customer_id = await _mediator.Send(command);
            return CreatedAtAction("GetById", "CustomersCommandController", new { id = customer_id, command });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
            long customer_id = await _mediator.Send(command);
            return Ok(customer_id);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            long customer_id = await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return Ok(customer_id);
        }


    }
}