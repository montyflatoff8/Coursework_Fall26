using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;
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
            return context.Categories.ToList();
        }
    }
}
