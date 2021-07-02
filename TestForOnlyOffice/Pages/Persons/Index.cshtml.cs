using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class IndexModel : PageModelBase
    {
        private readonly IPersonManager _personManager;

        public IndexModel(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [BindProperty]
        public IEnumerable<Person> PersonList { get; set; }

        [BindProperty]
        public List<Person> Person { get; set; }

        public void OnGet()
        {
            PersonList = _personManager.GetPersonList();
        }
    }
}
