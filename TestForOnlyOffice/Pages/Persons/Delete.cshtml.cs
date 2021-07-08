using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using TestForOnlyOffice.Classes;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class DeleteModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<DeleteModel> _logger;

        public DeleteModel(IPersonManager personManager, ILogger<DeleteModel> logger)
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
                _logger.LogError("Delete Page Error. Id is empty");
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                _logger.LogError("Delete Page Error. Person is not found");
                return NotFound();
            }

            _logger.LogInformation("Delete Page open");
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("Delete Page Error. Id is empty");
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                _logger.LogError("Delete Page Error. Person is not found");
                return NotFound();
            }

            _personManager.Delete(Person.Id);
            _logger.LogInformation("Person deleted");

            return RedirectToPage("./Index");
        }
    }
}
