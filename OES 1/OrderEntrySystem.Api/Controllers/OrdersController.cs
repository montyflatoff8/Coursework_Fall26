using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orders;

        public OrdersController(IOrderRepository orders)
        {
            this.orders = orders;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(orders.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = orders.GetById(id);
            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Order>> Post(Order order)
        {
            Order created = orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, Order order)
        {
            var updated = orders.Update(id, order);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            var deleted = orders.Delete(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}