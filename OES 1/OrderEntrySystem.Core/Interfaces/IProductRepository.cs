using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
   public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product Add(Product product);

        Product? GetById(int id);

        Product? Update(int id, Product updatedProduct);

        Product? Delete(int id);
    }
}
