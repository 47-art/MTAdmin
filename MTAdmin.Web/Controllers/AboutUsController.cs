using Microsoft.AspNetCore.Mvc;
using MTClient.Application.Services.Interfaces;

namespace MTAdmin.Web.Controllers
{    
    public class AboutUsController : ClientBaseController
    {
        private readonly IAboutUsService _service;
        public AboutUsController(IAboutUsService service)
        {
            _service = service;
        }
        [Route("about-us")]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAboutUsMetaData();
            AddMetaData(result.Data);
            return View();
        }
    }
}
