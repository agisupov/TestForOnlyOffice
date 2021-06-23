using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly IPersonManager _personManager;

        public EditModel(IPersonManager personManager)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
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

            return RedirectToPage("./Index");
        }
    }
}
