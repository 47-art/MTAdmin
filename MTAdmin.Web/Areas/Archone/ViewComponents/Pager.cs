using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.Areas.Archone.ViewComponents
{
    public class Pager : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedBase pagedBase)
        {
            return await Task.FromResult((IViewComponentResult)View("Default", pagedBase));
        }
    }
}
