using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Assignment3Client.ViewModels
{
    public class BillPayViewModel
    {
        public List<SelectListItem> Status { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Blocked", Text = "Blocked" },

        };
    }
}
