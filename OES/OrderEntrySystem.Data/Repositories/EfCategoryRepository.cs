using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly OESContext context;

        public EfCategoryRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories
                .Include(c => c.ProductCategories)
                .Where(c => !c.IsArchived)
                .ToList();
        }

        public Category? Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public Category? GetByID(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category? Update(int id, Category updatedCategory)
        {
            var existing = GetByID(id); //grab the matching category from the database
            if (existing == null)
            {
                return null;
            }
            // Replace the properties of the existing category with the updated values
            existing.Name = updatedCategory.Name;
            context.SaveChanges();
            return existing;
        }

        public Category? Delete(int id)
        {
            var category = context.Categories
                .Include(c => c.ProductCategories)
                .FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                category.IsArchived = true;

                foreach (var link in category.ProductCategories.Where(pc => !pc.IsArchived))
                {
                    link.IsArchived = true;
                }

                context.SaveChanges();
            }

            return category;
        }
    }
}
