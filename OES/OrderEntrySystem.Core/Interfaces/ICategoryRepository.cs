using OrderEntrySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category? GetByID(int id);

        Category? Add(Category category);

        Category? Update(int id, Category updatedCategory);

        Category? Delete(int id);
    }
}
