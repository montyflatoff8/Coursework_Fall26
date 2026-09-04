using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Api.Repositories;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customers;

        public CustomersController(ICustomerRepository customers)
        {
            this.customers = customers;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(customers.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = customers.GetByID(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Customer>> Post(Customer customer)
        {
            Customer? created = customers.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = created?.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, Customer customer)
        {
            var updated = customers.Update(id, customer);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var deleted = customers.Delete(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
