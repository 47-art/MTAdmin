using Microsoft.AspNetCore.Mvc;

namespace MTAdmin.Web.Controllers
{
    public class TagsController : ClientBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
