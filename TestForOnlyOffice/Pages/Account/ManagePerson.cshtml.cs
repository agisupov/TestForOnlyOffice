using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class ManagePersonModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private Dictionary<string, string> languages = new Dictionary<string, string>
        {
            {"Russian", "ru"},
            {"English", "en"}
        };

        public ManagePersonModel(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [BindProperty]
        public Person Person { get; set; }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Person = _personManager.Update(Person);

            if (Person == null)
            {
                return NotFound();
            }

            await UpdateLocalityClaim();
            return RedirectToPage("/Index");
        }

        public async Task UpdateLocalityClaim()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claim = User.FindFirst(ClaimTypes.Locality);
            if (claim != null)
                identity.RemoveClaim(claim);
            identity.AddClaim(new Claim(ClaimTypes.Locality, languages[Person.Language]));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
