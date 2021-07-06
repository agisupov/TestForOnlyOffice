using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IPersonManager _personManager;

        public RegisterModel(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [BindProperty]
        public Person Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var person = new Person();
                person.FirstName = Input.FirstName;
                person.LastName = Input.LastName;
                person.Email = Input.Email;
                person.Password = Input.Password;
                person.Language = null;

                _personManager.Create(person);
                return LocalRedirect(returnUrl);
            }
            return Page();
        }
    }
}
