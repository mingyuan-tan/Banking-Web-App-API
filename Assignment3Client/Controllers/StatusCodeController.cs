﻿using Microsoft.AspNetCore.Mvc;

namespace Assignment3Client.Controllers
{
    public class StatusCodeController : Controller
    {
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            return View(statusCode);
        }
    }
}