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
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private ApplicationDbContext _db;

        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Person Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                Person person = await _db.Person.FirstOrDefaultAsync(x => x.Email == Input.Email);

                if (person == null)
                {
                    person = new Person();
                    person.FirstName = Input.FirstName;
                    person.LastName = Input.LastName;
                    person.Email = Input.Email;
                    person.Password = Input.Password;
                    person.Language = Input.Language;

                    _db.Person.Add(person); //may be IPersonManager.Create(person)?
                    await _db.SaveChangesAsync();
                    await Authenticate(Input.Email);
                    return LocalRedirect(returnUrl);
                }
                else
                    ModelState.AddModelError("", "Data is not correct");
            }
            return Page();
        }

        public async Task Authenticate(string personName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, personName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
