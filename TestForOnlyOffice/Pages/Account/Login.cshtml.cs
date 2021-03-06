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
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class LoginModel : PageModel
    {
        private IPersonManager _personManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IPersonManager personManager, ILogger<LoginModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public Person Person { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "EmailRequired")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "EmailAddress")]
            public string Email { get; set; }

            [Required(ErrorMessage = "PasswordRequired")]
            [MinLength(6, ErrorMessage = "PasswordLength")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
            _logger.LogInformation("Login Page is open");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = Url.Content("~/");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Login Page Error. Model state is not valid");
                return Page();
            }
            Person = _personManager.Login(Input.Email, Input.Password);
            if (Person != null)
            {
                await Authenticate(Person);
                _logger.LogInformation($"Person {Person.FirstName} {Person.LastName} is authenticated");
                return LocalRedirect(returnUrl);
            }
            else
            {
                _logger.LogError("Login Page Error. Invalid login attempt");
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        public async Task Authenticate(Person person)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, person.Email),
                new Claim(ClaimTypes.Name, person.FirstName + " " + person.LastName),
                new Claim(ClaimTypes.Locality, person.Language),
                new Claim(ClaimTypes.Sid, person.Id.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
