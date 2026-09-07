using OrderEntrySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface IOrderLineRepository
    {
        IEnumerable<OrderLine> GetAll();

        IEnumerable<int> GetOrderIdsWithLines();

        OrderLine Add(OrderLine orderLine);

        OrderLine? GetById(int id);

        OrderLine? Update(int id, OrderLine updatedOrderLine);

        OrderLine? Delete(int id);
    }
}
