using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _service;
        public ContactController(IToastNotification notification, IContactService service) : base(notification)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            if(DateTime.TryParse(s,out DateTime createdDate))
            {
                s = createdDate.ToUniversalTime().Date.ToString("yyyy-MM-dd");
            }
            var parameters = new ContactParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetList(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
        public async Task<IActionResult> GetMessage(int Id)
        {
            var result = await _service.GetContactMessage(Id);
            return PartialView("~/Views/Contact/_ContactMessage.cshtml", result.Data);
        }
    }
}