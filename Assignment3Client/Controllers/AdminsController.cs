using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WDT_Assignment2.Models;
using Assignment3Client.ViewModels;
using System.Net.Http;
using System.Text;

namespace Assignment3Client.Controllers
{
    [AuthorizeAdmin]
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

        public async Task<IActionResult> EditCustomerProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await BankAPI.InitializeClient().GetAsync($"api/Customers/{id}"); 

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            ViewData["States"] = new ProfileViewModel().States;
            ViewData["Status"] = new ProfileViewModel().Status;

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomerProfile(int id, Customer customer)
        {
            if(id != customer.CustomerID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = BankAPI.InitializeClient().PutAsync("api/Customers", content).Result; 

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); 
                } 
            }

            ViewData["States"] = new ProfileViewModel().States;
            ViewData["Status"] = new ProfileViewModel().Status;

            return View(customer);
        }

    }
}