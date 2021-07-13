using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Account
{
    public class ManagePersonModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<ManagePersonModel> _logger;
        private Dictionary<string, string> languages = new Dictionary<string, string>
        {
            {"Russian", "ru"},
            {"English", "en"}
        };

        public ManagePersonModel(IPersonManager personManager, ILogger<ManagePersonModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        [BindProperty]
        public Person Person { get; set; }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("Manage Person Page Error. Id is empty");
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                _logger.LogError("Manage Person Page Error. Person is not found");
                return NotFound();
            }

            _logger.LogInformation("Manage Person is open");
            return Page();
        }

        public async Task<IActionResult> OnPostSave()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error. ModelState is not valid");
                return Page();
            }

            UpdatePerson();

            if (Person == null)
            {
                _logger.LogError("Manage Person Page Error. Person is not found");
                return NotFound();
            }

            await UpdateLocalityClaim();
            _logger.LogInformation("Person update data");
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostUpload()
        {
            if (Person.AvatarFile != null)
            {
                Person.Avatar = await GetByteArrayFromImage(Person.AvatarFile);
            }
            UpdatePerson();
            return RedirectToPage(new { id = Person.Id });
        }

        public void UpdatePerson()
        {
            Person.Language = languages[Person.Language];
            Person = _personManager.Update(Person);
        }

        public async Task UpdateLocalityClaim()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claim = User.FindFirst(ClaimTypes.Locality);
            if (claim != null)
                identity.RemoveClaim(claim);
            identity.AddClaim(new Claim(ClaimTypes.Locality, Person.Language));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        private async Task<byte[]> GetByteArrayFromImage(IFormFile imgFile)
        {
            using var stream = new MemoryStream();
            await imgFile.CopyToAsync(stream);
            if (stream.Length <= 5242880)
            {
                _logger.LogInformation("File is ready to upload");
                return stream.ToArray();
            }
            else
            {
                _logger.LogError("Error. File more than 5 MB");
                return null;
            }
        }

        public string GetImage()
        {
            string img64 = Convert.ToBase64String(Person.Avatar);
            string img64Url = string.Format($"data:image/jpg;base64,{img64}");
            return img64Url;
        }
    }
}
