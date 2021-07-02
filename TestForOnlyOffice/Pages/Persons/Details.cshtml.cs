﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Pages.Persons
{
    public class DetailsModel : PageModelBase
    {
        private readonly IPersonManager _personManager;

        public DetailsModel(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public Person Person { get; set; }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
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
    }
}
