using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly IPersonManager _personManager;

        public CreateModel(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _personManager.Create(Person);
            _personManager.Save();

            return RedirectToPage("./Index");
        }
    }
}
