using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{

    public class CreateModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<CreateModel> _logger;

        public CreateModel(IPersonManager personManager, ILogger<CreateModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            _logger.LogInformation("Create Model Page open");
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Create Model Page Error. ModelState is not valid");
                return Page();
            }

            _personManager.Create(Person);
            _logger.LogInformation("Person created");
            return RedirectToPage("./Index");
        }
    }
}
