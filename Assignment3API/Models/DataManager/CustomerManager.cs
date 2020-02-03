using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3API.Data;
using Assignment3API.Models.Repository;


namespace Assignment3API.Models.DataManager
{
    public class CustomerManager : IDataRepository<Customer, int>
    {
        private readonly NwbaContext _context;

        public CustomerManager(NwbaContext context)
        {
            _context = context;
        }


        // Returning customer by ID - SQL: select * from customer where customerid is <> 
        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }


        // Simple loading and getting all the data 
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }


        // Inserts data and generate a new customer 
        public int Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.CustomerID;
        }

        // Deleting the data based on the ID 
        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }


        public int Update (int id, Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }
    }
}
