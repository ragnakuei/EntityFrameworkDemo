using System.Linq;
using System.Web.Mvc;
using EntityFrameworkDemo.Helpers;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserInfo _userInfo;

        public HomeController(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public ActionResult Index()
        {
            var cultureCookie = Request.Cookies["_culture"]?.Value ?? "(None)";

            ViewBag.CookieCulture = cultureCookie;

            ViewData["LangSelectList"] = CultureHelper.GetAllImplementedCultures()
                                                      .Select(c =>
                                                              {
                                                                  var item = new SelectListItem();
                                                                  item.Text = c.Value;
                                                                  item.Value = c.Value;
                                                                  item.Selected = c.Value.ToLower() == cultureCookie.ToLower();
                                                                  return item;
                                                              });


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set(CultureVM cultureVm)
        {
            CultureHelper.SetCulture(HttpContext, cultureVm.Language);
            return RedirectToAction("Index");
        }

    }
}