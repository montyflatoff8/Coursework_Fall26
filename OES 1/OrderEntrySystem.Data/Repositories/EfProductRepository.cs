using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;
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
            return context.Products.Include(p => p.Category).ToList(); // need .Include so that Ef knows to populate it.
        }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product? GetById(int id)
        {
            return context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existing = context.Products.FirstOrDefault(p => p.Id == id); //grab the matching product from the database

            if (existing == null)
            {
                return null;
            }

            // Replace the properties of the existing product with the updated values
            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Quantity = updatedProduct.Quantity;
            existing.Price = updatedProduct.Price;
            existing.CategoryId = updatedProduct.CategoryId;

            context.SaveChanges();
            return existing;
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
