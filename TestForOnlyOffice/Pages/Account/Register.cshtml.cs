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
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<RegisterModel> _logger;

        public RegisterModel(IPersonManager personManager, ILogger<RegisterModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        [BindProperty]
        public Person Input { get; set; }

        public IActionResult OnGet()
        {
            _logger.LogInformation("Register person");
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
                _logger.LogInformation("Person added");
                return LocalRedirect(returnUrl);
            }
            _logger.LogError("Person not added");
            return Page();
        }
    }
}
