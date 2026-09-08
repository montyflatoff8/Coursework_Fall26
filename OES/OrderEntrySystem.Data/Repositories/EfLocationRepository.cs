using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfLocationRepository : ILocationRepository
    {
        private readonly OESContext context;

        public EfLocationRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return context.Locations.ToList();
        }

        public Location? Add(Location location)
        {
            context.Add(location);
            context.SaveChanges();
            return location;
        }

        public Location? GetByID(int id)
        {
            return context.Locations.FirstOrDefault(l => l.Id == id);
        }

        public Location? Update(int id, Location updatedLocation)
        {
            var existing = context.Locations.FirstOrDefault(l => l.Id == id); //grab the matching location from the database

            if (existing == null)
            {
                return null;
            }

            // Replace the properties of the existing location with the updated values
            existing.Name = updatedLocation.Name;
            existing.City = updatedLocation.City;
            existing.State = updatedLocation.State;

            context.SaveChanges();
            return existing;
        }

        public Location? Delete(int id)
        {
            var location = context.Locations.FirstOrDefault(l => l.Id == id); //grab the matching location from the database

            if (location != null)
            {
                context.Locations.Remove(location);
                context.SaveChanges();
            }
            return location;
        }
    }
}
