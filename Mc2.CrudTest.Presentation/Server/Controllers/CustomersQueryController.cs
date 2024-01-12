using Mc2.CrudTest.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using Mc2.CrudTest.Core.Domain.Customers.DTOs;
using Mc2.CrudTest.Core.Domain.Customers.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersQueryController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;

        public CustomersQueryController(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var mediator = new Mediator(serviceProvider);
            var customers = await mediator.Send(new GetAllCustomersQuery());
            Console.WriteLine("get all: " + customers.FirstOrDefault()?.FirstName);
            return customers;
        }

        [HttpGet]
        [Route("GetAllCustomers/{Id}")]
        public CustomerDto? GetCustomerById([FromForm] long id)
        {
            var list = new List<CustomerDto>
            {
                new CustomerDto{
                    Id = 1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    DateOfBirth = DateTime.Now,
                    PhoneNumber = "PhoneNumber",
                    Email = "Email",
                    BankAccountNumber = "BankAccountNumber",
                },
                new CustomerDto
                {
                    Id = 2,
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    DateOfBirth = DateTime.UtcNow,
                    PhoneNumber = "PhoneNumber",
                    Email = "Email",
                    BankAccountNumber = "BankAccountNumber",
                }
            };
            Console.WriteLine("get by id: " + id);

            return list.Where(x => x.Id == id).FirstOrDefault();
        }


    }
}
