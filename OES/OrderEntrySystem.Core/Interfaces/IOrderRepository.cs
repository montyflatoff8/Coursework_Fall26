using OrderEntrySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order Add(Order order);

        Order? GetById(int id);

        Order? Update(int id, Order updatedOrder);

        Order? Delete(int id);
    }
}
