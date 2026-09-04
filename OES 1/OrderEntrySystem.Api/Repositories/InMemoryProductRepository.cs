using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;

namespace OrderEntrySystem.Api.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
                
        public InMemoryProductRepository()
        {
            products.Add(new Product { Id = 1, Name = "Mug", Description = "A ceramic mug for beverages", Quantity = 50, Price = 12.99m });
            products.Add(new Product { Id = 2, Name = "Bottle", Description = "A stainless steel water bottle", Quantity = 30, Price = 19.99m });
            products.Add(new Product { Id = 3, Name = "Bowl", Description = "A ceramic bowl for food", Quantity = 20, Price = 9.99m });
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product Add(Product product)
        {
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1; // checks if the list is empty, if it is, id is 1, if not, id is max id + 1
            products.Add(product);
            return product;
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id); // returns the first product with the matching id, or null if not found
        }

        public Product? Update(int id, Product product)
        {
            return product; // blank for now
        }

        public Product? Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }
            return product;
        }
    }
}
