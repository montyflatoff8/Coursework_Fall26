using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Enumerations
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
}
