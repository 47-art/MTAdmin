using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Interfaces;

namespace MTAdmin.Web.Controllers
{
    public class SearchController : ClientBaseController
    {
        private readonly ITemplateService _service;
        public SearchController(ITemplateService service)
        {
            _service = service;
        }
        [Route("search-meme-templates")]
        public async Task<IActionResult> Index()
        {
            var metaTags = await _service.GetSearchPageMetaData();
            AddMetaData(metaTags.Data);
            return View();
        }
        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> Search(TemplateParams @params)
        {
            var result = await _service.GetTemplates(@params);
            if (result.IsSuccess)
            {
                int i = 0;
                while (i < result.Data.TemplateList.Items.Count)
                {
                    result.Data.TemplateList.Items[i].TemplateType = GetFileContentType(result.Data.TemplateList.Items[i].FileName);
                    i++;
                }
            }
            return PartialView("~/Views/Search/SearchResultPartial.cshtml", result.Data);
        }
    }
}
