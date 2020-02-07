using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Assignment3Client.Models;

namespace Assignment3Client.Attributes
{
    public class AuthorizeAdmin : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var AdminID = context.HttpContext.Session.GetInt32(nameof(AdminLogin.AdminLoginID));
            if (!AdminID.HasValue)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
