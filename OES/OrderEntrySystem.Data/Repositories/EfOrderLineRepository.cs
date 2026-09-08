using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Data.DataAccess;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfOrderLineRepository : IOrderLineRepository
    {
        // Match whatever your DbContext type is actually called — the same
        // one your EfProductRepository / EfOrderRepository classes inject.
        private readonly OESContext context;

        public EfOrderLineRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return context.OrderLines
                .Include(ol => ol.Product)
                .ToList();
        }

        public IEnumerable<int> GetOrderIdsWithLines()
        {
            return context.OrderLines
                .Select(ol => ol.OrderId)
                .Distinct()
                .ToList();
        }

        public OrderLine? GetById(int id)
        {
            return context.OrderLines
                .Include(ol => ol.Product)
                .FirstOrDefault(ol => ol.Id == id);
        }

        public OrderLine Add(OrderLine orderLine)
        {
            context.OrderLines.Add(orderLine);
            context.SaveChanges();
            return GetById(orderLine.Id)!;
        }

        public OrderLine? Update(int id, OrderLine updatedOrderLine)
        {
            var existing = context.OrderLines.FirstOrDefault(ol => ol.Id == id);
            if (existing == null) return null;

            existing.ProductId = updatedOrderLine.ProductId;
            existing.Quantity = updatedOrderLine.Quantity;

            context.SaveChanges();
            return GetById(id);
        }

        public OrderLine? Delete(int id)
        {
            var existing = context.OrderLines.FirstOrDefault(ol => ol.Id == id);
            if (existing == null) return null;

            context.OrderLines.Remove(existing);
            context.SaveChanges();
            return existing;
        }
    }
}