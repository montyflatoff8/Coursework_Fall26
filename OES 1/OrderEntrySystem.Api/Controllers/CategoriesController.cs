using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;

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
    }
}
