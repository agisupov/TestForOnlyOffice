using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class IndexModel : PageModel
    {
        private readonly IPersonManager _personManager;
        private ILogger<IndexModel> _logger;

        public IndexModel(IPersonManager personManager, ILogger<IndexModel> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }

        [BindProperty]
        public IEnumerable<Person> PersonList { get; set; }

        [BindProperty]
        public List<Person> Person { get; set; }

        public void OnGet()
        {
            PersonList = _personManager.GetPersonList();
            _logger.LogInformation("Get Person List");
        }
    }
}
