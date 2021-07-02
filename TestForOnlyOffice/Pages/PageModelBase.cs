using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Data;

namespace TestForOnlyOffice.Pages
{
    public class PageModelBase : PageModel
	{
		public Dictionary<string, string> languages = new Dictionary<string, string>
		{
			{"Russian", "ru"},
			{"English", "en"}
		};

		public string Culture { get; set; }

		public IActionResult SetLanguage(string culture)
		{
			string returnUrl = Url.Content("~/");
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);
			Culture = culture;

			return LocalRedirect(returnUrl);
		}
	}
}
