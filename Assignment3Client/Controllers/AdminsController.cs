using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WDT_Assignment2.Models;

namespace Assignment3Client.Controllers
{
    public class AdminsController : Controller
    {


        public async Task<IActionResult> Index()
        {
            var response = await BankAPI.InitializeClient().GetAsync("api/Customers");

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            // Storing response details received from the web api 
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response reveived from web api and storing into a list 
            var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            return View(customers);
        }
    }
}