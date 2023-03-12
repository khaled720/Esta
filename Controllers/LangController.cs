using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ESTA.Controllers
{
    public class LangController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;

        public LangController(IStringLocalizer<ESTA.SharedResource> localizer)
        {
            this.localizer = localizer;
        }
        [HttpPost]
        public IActionResult SetCulture(string culture)
        {
            try
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
                );
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


        [HttpGet]
        public JsonResult GetCulture()
        {
            try
            {
                return Json(Thread.CurrentThread.CurrentCulture.Name);
            }
            catch (Exception)
            {
                return Json("en");
            }
        }

        [HttpGet]
        public JsonResult GetLocalizedString(string key)
        {
            try
            {
                return Json(localizer[key]);

            }
            catch (Exception)
            {
                return Json(key);
            }
        }

    }
}
