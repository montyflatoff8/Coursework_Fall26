using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly IOrderLineRepository orderLineRepository;

        public OrderLinesController(IOrderLineRepository orderLineRepository)
        {
            this.orderLineRepository = orderLineRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderLine>> GetAll()
        {
            return Ok(orderLineRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OrderLine> GetById(int id)
        {
            var orderLine = orderLineRepository.GetById(id);
            if (orderLine == null) return NotFound();
            return Ok(orderLine);
        }

        [HttpGet("order-ids")]
        public ActionResult<IEnumerable<int>> GetOrderIdsWithLines()
        {
            return Ok(orderLineRepository.GetOrderIdsWithLines());
        }

        [HttpPost]
        public ActionResult<OrderLine> Add(OrderLine orderLine)
        {
            var created = orderLineRepository.Add(orderLine);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<OrderLine> Update(int id, OrderLine updatedOrderLine)
        {
            var result = orderLineRepository.Update(id, updatedOrderLine);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<OrderLine> Delete(int id)
        {
            var result = orderLineRepository.Delete(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}