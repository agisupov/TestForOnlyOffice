using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<EditModel> _logger;

        public EditModel(IPersonManager personManager, ILogger<EditModel> logger)
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
                _logger.LogError("Edit Page Error. Id is empty");
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                _logger.LogError("Edit Page Error. Person is not found");
                return NotFound();
            }
            _logger.LogInformation("Edit Model Page open");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Edit Model Page Error. ModelState is not valid");
                return Page();
            }

            Person = _personManager.Update(Person);

            if (Person == null)
            {
                _logger.LogError("Edit Page Error. Person is not found");
                return NotFound();
            }

            _logger.LogInformation("Person updated");
            return RedirectToPage("./Index");
        }
    }
}
