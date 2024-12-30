using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.Constants;
using MTAdmin.Shared.Models;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    [Area("Archone")]
    [Authorize(Roles = Roles.Archon)]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {
        internal readonly IToastNotification _toastNotification;

        public BaseController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        internal void ShowMessage<T>(Response<T> response) where T : notnull 
        {
            if (!response.IsSuccess)
            {
                _toastNotification.AddErrorToastMessage(response.Message);
            }
            else if(response.IsSuccess && response.Message.Any())
            {
                _toastNotification.AddSuccessToastMessage(response.Message);
            }
        }
    }
}
