using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Assignment3API.Models;
using Assignment3API.Models.DataManager;

namespace Assignment3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillPaysController : ControllerBase
    {
        // Create an instance of the manager class and pass it to the constructor 
        private readonly AdminManager _repo;

        // This is a constructor dependency injection 
        public BillPaysController(AdminManager repo)
        {
            _repo = repo;
        }

        // GET: api/BillPay - get all the BillPay 
        // Notice that in this method we are not writing any query. All the logic is handled 
        // by the manager file 
        [HttpGet]
        public IEnumerable<BillPay> Get()
        {
            return _repo.GetAllBillPays();
        }

        // GET api/BillPays/1
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.GetBillPay(id);
        }

        // PUT api/movies 
        // Insert a new movie 
        [HttpPut]
        public void Put([FromBody] BillPay billPay)
        {
            _repo.UpdateBillPay(billPay.BillPayID, billPay);
        }
    }
}
