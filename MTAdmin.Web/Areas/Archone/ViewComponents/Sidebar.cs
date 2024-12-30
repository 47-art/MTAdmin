using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.Helpers;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.Areas.Archone.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        private readonly ICurrentUserService _service;
        public Sidebar(ICurrentUserService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync(string filter)
        {
            var sidebars = new List<SidebarMenu>();

            if (_service.IsUserAuthenticated())
            {
                sidebars = new List<SidebarMenu>
                {
                    ModuleHelper.AddModule(Shared.Enums.Module.Dashboard),
                    ModuleHelper.AddModule(Shared.Enums.Module.Languages),
                    ModuleHelper.AddModule(Shared.Enums.Module.Categories),
                    ModuleHelper.AddModule(Shared.Enums.Module.Tags),
                    ModuleHelper.AddModule(Shared.Enums.Module.Templates),
                    ModuleHelper.AddModule(Shared.Enums.Module.Enquiries),
                    ModuleHelper.AddModule(Shared.Enums.Module.UserLogs)
                };
            }
            
            return await Task.FromResult((IViewComponentResult)View("Default", sidebars));
        }
    }
}
