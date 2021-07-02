using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModelBase
    {
        private ApplicationDbContext _db;

        public LogoutModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Person Person { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string returnUrl = Url.Content("~/");
            Guid id = Guid.Parse(HttpContext.Request.Cookies["id"]);
            Person = _db.Person.Find(id);
            Person.Language = Culture;
            _db.SaveChanges();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            base.SetLanguage("en");
            return RedirectToAction("Login", "Account");
        }
    }
}
