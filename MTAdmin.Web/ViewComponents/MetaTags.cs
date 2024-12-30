using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.ViewComponents
{
    public class MetaTags : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MetaDataModel model = new MetaDataModel()
            {
                MetaTitle = "The MemeTemplate",
                MetaDescription = "The MemeTemplate",
                MetaKeywords = "The MemeKeywords"
            };
            if (ViewData["MetaInfo"] != null)
            {
                model = ViewData["MetaInfo"] as MetaDataModel;
            }
            return await Task.FromResult((IViewComponentResult)View("Default",model));
        }
    }
}
