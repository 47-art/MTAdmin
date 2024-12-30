using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Interfaces;

namespace MTAdmin.Web.Controllers
{
    public class ContactController : ClientBaseController
    {
        private readonly IContactService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public ContactController(IContactService service, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _service = service;
        }
        [Route("contact")]
        public IActionResult Index()
        {
            //var metaTags = await _service.GetContactPageMetaData();
            //AddMetaData(metaTags.Data);
            return View(new ContactVM());
        }
        [HttpPost]
        [Route("thank-you")]
        public async Task<IActionResult> ThankYou(ContactVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.IPAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //var metaTags = await _service.GetContactPageMetaData();
                //AddMetaData(metaTags.Data);
                await _service.AddContact(vm);              
            }
            return View();
        }
    }
}
