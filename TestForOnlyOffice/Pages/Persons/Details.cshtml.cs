using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class DetailsModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<DetailsModel> _logger;

        public DetailsModel(IPersonManager personManager, ILogger<DetailsModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        public Person Person { get; set; }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("Details Page Error. Id is empty");
                return NotFound();
            }

            Person = _personManager.GetPerson(id);

            if (Person == null)
            {
                _logger.LogError("Details Page Error. Person is not found");
                return NotFound();
            }
            _logger.LogInformation("Details Model Page open");
            return Page();
        }
    }
}
