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

        public IActionResult OnGet(int? id)
        {
            if (id == null)
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

            _personManager.Update(Person);

            try
            {
                _personManager.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_personManager.PersonExists(Person.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
