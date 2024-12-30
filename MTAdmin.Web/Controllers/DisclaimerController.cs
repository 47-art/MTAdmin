using Microsoft.AspNetCore.Mvc;

namespace MTAdmin.Web.Controllers
{
    public class DisclaimerController : ClientBaseController
    {
        [Route("disclaimer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
