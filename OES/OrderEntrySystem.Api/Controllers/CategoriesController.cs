using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categories;

        public CategoriesController(ICategoryRepository categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Ok(categories.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetByID(int id)
        {
            var category = categories.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Category>> Post(Category category)
        {
            categories.Add(category);
            return CreatedAtAction(nameof(GetByID), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            var updated = categories.Update(id, category);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            var deleted = categories.Delete(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
