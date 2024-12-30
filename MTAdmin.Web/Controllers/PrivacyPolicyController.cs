using Microsoft.AspNetCore.Mvc;

namespace MTAdmin.Web.Controllers
{
    public class PrivacyPolicyController : ClientBaseController
    {
        [Route("privacy-policy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
