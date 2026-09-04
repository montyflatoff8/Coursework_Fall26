using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Core.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer? Add(Customer customer);
        Customer? GetByID(int id);
        Customer? Update(int id, Customer updatedCustomer);
        Customer? Delete(int id);
    }
}
