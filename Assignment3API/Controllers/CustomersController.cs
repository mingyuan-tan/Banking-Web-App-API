using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3API.Data;
using Assignment3API.Models;
using Assignment3API.Models.DataManager;


namespace Assignment3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Create an instance of the manager class and pass it to the constructor 
        private readonly AdminManager _repo;

        // This is a constructor dependency injection 
        public CustomersController(AdminManager repo)
        {
            _repo = repo;
        }


        // Calls repo method to get all transactions of user from specified date
        [HttpGet("getCustomerTransactions/{id}/{start}/{end}")]
        public IEnumerable<Transaction> GetCustomerTransactions (int id, string start, string end)
        {
            //return new { id, start, end };

            return _repo.GetCustomerTransactions(id, DateTime.ParseExact(start, "dd-MM-yyyy", null), DateTime.ParseExact(end, "dd-MM-yyyy", null));
        }

        [HttpGet("getCustomerBillPays/{id}")]
        public IEnumerable<BillPay> GetCustomerBillPays(int id)
        {
            return _repo.GetCustomerBillPays(id);
        }


        [HttpGet("getCustomerFromBillPay/{id}")] 
        public int GetCustomerFromBillPay(int id)
        {
            return _repo.GetCustomerFromBillPay(id);
        }


        // GET: api/customers - get all the customers 
        // Notice that in this method we are not writing any query. All the logic is handled 
        // by the manager file 
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _repo.GetAll();
        }


        // GET api/customers/1
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _repo.Get(id);
        }


        // POST api/customers 
        // Usually you want to return the status of the post (whether it succeeded or failed) 
        // Normally used for updating 
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }


        // PUT api/movies 
        // Insert a new movie 
        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }

        // DELETE api/customers/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

        [HttpGet("getChartData/{id}/{start}/{end}")]
        public List<object> GetChartData(int id, string start, string end)
        {
            return _repo.GetChartData(id, DateTime.ParseExact(start, "dd-MM-yyyy", null), DateTime.ParseExact(end, "dd-MM-yyyy", null));
        }



































        // -------------------------------- AUTOMATICALLY GENERATED - UNUSED AS WE'RE USING REPO PATTERN --------------------------------//

        //private readonly NwbaContext _context;

        //public CustomersController(NwbaContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Customers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        //{
        //    return await _context.Customers.ToListAsync();
        //}

        //// GET: api/Customers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return customer;
        //}

        //// PUT: api/Customers/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(int id, Customer customer)
        //{
        //    if (id != customer.CustomerID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(customer).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Customers
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (CustomerExists(customer.CustomerID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
        //}

        //// DELETE: api/Customers/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();

        //    return customer;
        //}

        //private bool CustomerExists(int id)
        //{
        //    return _context.Customers.Any(e => e.CustomerID == id);
        //}
    }
}
