using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using TestForOnlyOffice.Classes;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class DeleteModel : PageModelBase
    {
        private readonly IPersonManager _personManager;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteModel(IPersonManager personManager, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _personManager = personManager;
            _sharedLocalizer = sharedLocalizer;
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

        public IActionResult OnPost(Guid id)
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

            _personManager.Delete(Person.Id);

            return RedirectToPage("./Index");
        }
    }
}
