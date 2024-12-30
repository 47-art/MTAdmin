using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IToastNotification toastNotification) : base(toastNotification)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
