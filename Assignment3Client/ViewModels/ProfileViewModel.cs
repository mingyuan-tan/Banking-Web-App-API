using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WDT_Assignment2.Models;

namespace Assignment3Client.ViewModels
{
    public class ProfileViewModel
    {
        public Customer Customer { get; set; }

        public List<SelectListItem> States { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "VIC", Text = "VIC" },
            new SelectListItem { Value = "NSW", Text = "NSW" },
            new SelectListItem { Value = "QLD", Text = "QLD" },
            new SelectListItem { Value = "WA", Text = "WA" },
            new SelectListItem { Value = "SA", Text = "SA" },
            new SelectListItem { Value = "TAS", Text = "TAS" },
        };

        public List<SelectListItem> Status { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Locked", Text = "Locked" },
            
        };
    }
}
