using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Api.Repositories;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository products;

        public ProductsController(IProductRepository products)
        {
            this.products = products;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(products.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.GetById(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Product>> Post(Product product)
        {
            Product created = products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, Product product)
        {
            var updated = products.Update(id, product);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var deleted = products.Delete(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
