using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class UserAuditController : BaseController
    {
        private readonly IUserAuditService _service;
        public UserAuditController(IToastNotification notification,IUserAuditService service) : base(notification)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            var parameters = new UserAuditParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetUserAudits(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
