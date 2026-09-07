using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class OrdersListViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public HashSet<int> OrderIdsWithLines { get; set; } = new HashSet<int>();
    }
}