using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.Repositories
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly OESContext context;

        public EfCustomerRepository(OESContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer? Add(Customer customer)
        {
            context.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer? GetByID(int id)
        {
            return context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? Update(int id, Customer updatedCustomer)
        {
            var existing = context.Customers.FirstOrDefault(c => c.Id == id); //grab the matching customer from the database

            if (existing == null)
            {
                return null;
            }

            // Replace the properties of the existing customer with the updated values
            existing.Name = updatedCustomer.Name;
            existing.Address = updatedCustomer.Address;

            context.SaveChanges();
            return existing;
        }

        public Customer? Delete(int id)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == id); //grab the matching customer from the database

            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }
    }
}
