using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly OESContext context;

        public EfProductRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.Include(p => p.Category).Include(p => p.Location).ToList(); // need .Include so that Ef knows to populate it.
        }

        public Product? Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return GetById(product.Id);
        }

        public Product? GetById(int id)
        {
            return context.Products.Include(p => p.Category).Include(p => p.Location).FirstOrDefault(p => p.Id == id);
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existing = context.Products.FirstOrDefault(p => p.Id == id);

            if (existing == null)
            {
                return null;
            }

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Quantity = updatedProduct.Quantity;
            existing.Price = updatedProduct.Price;
            existing.CategoryId = updatedProduct.CategoryId;
            existing.Condition = updatedProduct.Condition;
            existing.LocationId = updatedProduct.LocationId;

            context.SaveChanges();
            return GetById(id);
        }

        public Product? Delete(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id); //grab the matching product from the database

            if (product!= null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }
    }
}
