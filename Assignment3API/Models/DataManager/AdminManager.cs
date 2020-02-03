﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3API.Data;
using Assignment3API.Models.Repository;


namespace Assignment3API.Models.DataManager
{
    public class AdminManager : IDataRepository<Customer, int>
    {
        private readonly NwbaContext _context;

        public AdminManager(NwbaContext context)
        {
            _context = context;
        }


        // Gets all Transactions for a user 
        public List<Transaction> GetCustomerTransactions(int id, DateTime start, DateTime end)
        {
            var customer = _context.Customers.Find(id);
            List<Transaction> transactions = new List<Transaction>(); 

            foreach(var account in customer.Accounts)
            {
                foreach(var transaction in account.Transactions)
                {
                    // Range of specified time period
                    if(transaction.ModifyDate >= start && transaction.ModifyDate <= end)
                        transactions.Add(transaction);
                }
            }

            return transactions; 
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
