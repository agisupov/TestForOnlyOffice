using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages
{
    public class PersonsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Person> Persons;

        public PersonsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task OnGet()
        {
            Persons = await _db.Person.ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Person.AddAsync(Person);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Persons");
            }
            else
            {
                return Page();
            }
        }
    }
}
