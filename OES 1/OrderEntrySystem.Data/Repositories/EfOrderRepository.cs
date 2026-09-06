using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly OESContext context;
        public EfOrderRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.Include(o => o.Customer).ToList();
        }

        public Order Add(Order order)
        {
            context.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order? GetById(int id)
        {
            return context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
        }

        public Order? Update(int id, Order updatedOrder)
        {
            var existing = context.Orders.FirstOrDefault(o => o.Id == id);

            if (existing == null)
            {
                return null;
            }

            existing.Status = updatedOrder.Status;
            existing.CustomerId = updatedOrder.CustomerId;

            context.SaveChanges();
            return GetById(id);
        }

        public Order? Delete(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id); //grab the matching order from the database

            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
            return order;
        }
    }
}
