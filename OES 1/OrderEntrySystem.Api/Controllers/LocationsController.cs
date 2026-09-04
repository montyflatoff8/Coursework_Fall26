using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderEntrySystem.Api.Repositories;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository locations;

        public LocationsController(ILocationRepository locations)
        {
            this.locations = locations;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            return Ok(locations.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Location> Get(int id)
        {
            var location = locations.GetByID(id);

            if (location is null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Location>> Post(Location location)
        {
            Location? created = locations.Add(location);
            return CreatedAtAction(nameof(Get), new { id = created?.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Location> Put(int id, Location location)
        {
            var updated = locations.Update(id, location);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult<Location> Delete(int id)
        {
            var deleted = locations.Delete(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
