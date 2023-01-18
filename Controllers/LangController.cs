using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class LangController : Controller
    {
        [HttpPost]
        public IActionResult SetCulture(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );
            return Json(true);
        }
    }
}
