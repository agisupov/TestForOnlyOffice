using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class DeleteModel : PageModel
    {
        private readonly IPersonManager _personManager;

        public DeleteModel(IPersonManager personManager)
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

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person != null)
            {
                _personManager.Delete(Person.PersonId);
                _personManager.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
