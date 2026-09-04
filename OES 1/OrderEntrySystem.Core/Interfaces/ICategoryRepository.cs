using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}
