using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3API.Data;
using Assignment3API.Models.Repository;
using Microsoft.AspNetCore.Cors;

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
            var transactions = _context.Transactions.Where(x => x.Account.CustomerID == id).ToList();
            List<Transaction> filteredTransactions = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                // Range of specified time period
                if (transaction.ModifyDate >= start && transaction.ModifyDate <= end)
                    filteredTransactions.Add(transaction);
            }


            return filteredTransactions;
        }

        public List<BillPay> GetCustomerBillPays(int id)
        {
            return  _context.BillPays.Where(x => x.Account.CustomerID == id).ToList();
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

        public IEnumerable<BillPay> GetAllBillPays()
        {

            return _context.BillPays.ToList();
        }

        public int UpdateBillPay(int id, BillPay billPay)
        {
            _context.Update(billPay);
            _context.SaveChanges();

            return id;
        }

        public BillPay GetBillPay(int id)
        {
            return _context.BillPays.Find(id);
        }

        public int GetCustomerFromBillPay(int id)
        {
            var billPay = _context.BillPays.Where(x => x.BillPayID == id).FirstOrDefault();

            var account = _context.Accounts.Where(x => x.AccountNumber == billPay.AccountNumber).FirstOrDefault();

            

            return account.CustomerID;
        }


        public List<object> GetChartData(int id, DateTime start, DateTime end)
        {

            var transactions = _context.Transactions.Where(x => x.Account.CustomerID == id).ToList();
            List<Transaction> filteredTransactions = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                // Range of specified time period
                if (transaction.ModifyDate >= start && transaction.ModifyDate <= end)
                    filteredTransactions.Add(transaction);
            }

            var data = filteredTransactions.GroupBy(x => new { group = x.TransactionType })
                                            .Select(group => new
                                            {
                                                transactionType = group.Key.group,
                                                count = group.Count()
                                            }).OrderByDescending(o => o.count).ToList();

            var labels = data.Select(x => x.transactionType).ToArray();
            var values = data.Select(x => x.count).ToArray();
            var maxValue = values[0];

            List<object> list = new List<object>();
            list.Add(labels);
            list.Add(values);
            list.Add(maxValue);

            return list;
        }
    }
}
