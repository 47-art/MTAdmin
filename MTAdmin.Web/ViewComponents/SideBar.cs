using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.Helpers;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.ViewComponents
{
    public class SideBar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sidebars = new List<SidebarMenu>
                {
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Home),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Categories),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Search),
//                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Tags),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.AboutUs),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Contact),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.Disclaimer),
                    ModuleHelper.AddClientModule(Shared.Enums.ClientModule.PrivacyPolicy)
                };
            return await Task.FromResult((IViewComponentResult)View("Default", sidebars));
        }
    }
}
