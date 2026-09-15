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
            return context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Location)
                .Where(p => !p.IsArchived)
                .ToList();
        }

        public Product? Add(Product product)
        {
            foreach (var categoryId in product.CategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    CategoryId = categoryId
                });
            }

            context.Products.Add(product);
            context.SaveChanges();
            return GetById(product.Id);
        }

        public Product? GetById(int id)
        {
            return context.Products
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Location)
                .FirstOrDefault(p => p.Id == id);
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existing = context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.Id == id);

            if (existing == null)
            {
                return null;
            }

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Quantity = updatedProduct.Quantity;
            existing.Price = updatedProduct.Price;
            existing.Condition = updatedProduct.Condition;
            existing.LocationId = updatedProduct.LocationId;

            var selectedCategoryIds = updatedProduct.CategoryIds ?? new List<int>();

            // Categories that were selected before but aren't anymore: archive the bridge row
            foreach (var link in existing.ProductCategories.Where(pc => !pc.IsArchived))
            {
                if (!selectedCategoryIds.Contains(link.CategoryId))
                {
                    link.IsArchived = true;
                }
            }

            // Categories that are selected now: add a new bridge row, or reactivate an archived one
            foreach (var categoryId in selectedCategoryIds)
            {
                var link = existing.ProductCategories.FirstOrDefault(pc => pc.CategoryId == categoryId);

                if (link == null)
                {
                    existing.ProductCategories.Add(new ProductCategory { CategoryId = categoryId });
                }
                else if (link.IsArchived)
                {
                    link.IsArchived = false;
                }
            }

            context.SaveChanges();
            return GetById(id);
        }

        public Product? Delete(int id)
        {
            var product = context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.IsArchived = true;

                foreach (var link in product.ProductCategories.Where(pc => !pc.IsArchived))
                {
                    link.IsArchived = true;
                }

                context.SaveChanges();
            }

            return product;
        }
    }
}
