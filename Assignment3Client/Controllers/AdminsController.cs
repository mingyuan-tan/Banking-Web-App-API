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
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> IndexToTransactions()
        {
            var response = await BankAPI.InitializeClient().GetAsync("api/Customers");
            // Storing response details received from the web api 
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response reveived from web api and storing into a list 
            var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            ViewData["CustomerID"] = new SelectList(customers, "CustomerID", "CustomerID"); 

            return View();
        }

       // [Route("Home/AdminThings5")]
        // Trying to get transactions within specified date parameters
        public async Task<IActionResult> ViewTransactions (int id, DateTime start, DateTime end)
        {
            var startFormatted = start.ToString("dd-MM-yyyy");
            var endFormatted = end.ToString("dd-MM-yyyy");

            var response = await BankAPI.InitializeClient().GetAsync($"api/Customers/getCustomerTransactions/{id}/{startFormatted}/{endFormatted}");
           
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            // Storing response details received from the web api 
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response received from web api and storing into a list 
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);

            return View(transactions);

        }



       public async Task<IActionResult> ViewBillPays(int id)

        {
            var response = await BankAPI.InitializeClient().GetAsync($"api/Customers/getCustomerBillPays/{id}");

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            // Storing response details received from the web api 
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response received from web api and storing into a list 
            var billPays = JsonConvert.DeserializeObject<List<BillPay>>(result);

            return View(billPays);
        }


        public async Task<IActionResult> EditBillPay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await BankAPI.InitializeClient().GetAsync($"api/BillPays/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var result = response.Content.ReadAsStringAsync().Result;
            var billpay = JsonConvert.DeserializeObject<BillPay>(result);

            ViewData["Status"] = new BillPayViewModel().Status;

            return View(billpay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBillPay(int id, BillPay billpay)
        {

            if (id != billpay.BillPayID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var content = new StringContent(JsonConvert.SerializeObject(billpay), Encoding.UTF8, "application/json");
                var response = BankAPI.InitializeClient().PutAsync("api/BillPays", content).Result;

                // These are to get customerID so it can be passed to ViewBillPays and get back into that page
                var response2 = BankAPI.InitializeClient().GetAsync($"api/Customers/getCustomerFromBillPay/{id}");
                int customerID = int.Parse(response2.Result.Content.ReadAsStringAsync().Result);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewBillPays", new { id = customerID });
                }
            }

            ViewData["Status"] = new BillPayViewModel().Status;


            return View(billpay);
        }

    }
}