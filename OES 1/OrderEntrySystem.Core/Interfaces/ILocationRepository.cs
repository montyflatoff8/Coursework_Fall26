using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
        Location? Add(Location location);
        Location? GetByID(int id);
        Location? Update(int id, Location updatedLocation);
        Location? Delete(int id);
    }
}
