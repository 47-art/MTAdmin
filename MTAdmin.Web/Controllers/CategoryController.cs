using Microsoft.AspNetCore.Mvc;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Interfaces;

namespace MTAdmin.Web.Controllers
{
    public class CategoryController : ClientBaseController
    {
        private readonly ICategoryService _service;
        private readonly ITemplateService _templateService;
        public CategoryController(ICategoryService service,ITemplateService templateService)
        {
            _service = service;
            _templateService = templateService;
        }
        [Route("{filter?}/meme-template-categories")]
        public async Task<IActionResult> Index(string filter = "trending")
        {
            var @params = new CategoryParams
            {
                FilterBy = CategoryFilterEnum.trending
            };
            if (!string.IsNullOrWhiteSpace(filter))
            {
                if(Enum.TryParse(filter, out CategoryFilterEnum filterBy))
                {
                    if (Enum.IsDefined(filterBy))
                    {
                        @params.FilterBy = filterBy;
                    }                    
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { filter = "trending" });
                }
            }
            var result = await _service.GetCategories(@params);
            var metaTags = await _service.GetCategoryMetaData();
            AddMetaData(metaTags.Data);
            return View(result.Data);
        }
        [Route("category/{id}/{filter?}/{slug}-meme-templats")]
        public async Task<IActionResult> Detail(int id,string slug, string filter = null)
        {            
            var @params = new TemplateParams()
            {
                Query = null,
                CategoryId = id,
                FilterBy = FilterByEnum.latest
            };
            if (!string.IsNullOrWhiteSpace(filter) && Enum.TryParse(filter, out FilterByEnum filterBy))
            {
                if (Enum.IsDefined(filterBy))
                {
                    @params.FilterBy = filterBy;
                }
            }
            if (@params.CategoryId <= 0 || !Enum.IsDefined(@params.FilterBy))
            {
                return View();
            }
            await _service.SaveCatogyPostViewState(id);
            var result = await _templateService.GetTemplates(@params);
            var metaTags = await _service.GetCategoryMetaDataById(@params.CategoryId);
            if (result.IsSuccess)
            {
                result.Data.CategorySlug = slug;
                int i = 0;
                while (i < result.Data.TemplateList.Items.Count)
                {
                    result.Data.TemplateList.Items[i].TemplateType = GetFileContentType(result.Data.TemplateList.Items[i].FileName);
                    i++;
                }
            }
            if (metaTags.IsSuccess)
            {
                AddMetaData(metaTags.Data);
            }
            return View(result.Data);
        }
        [HttpPost]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories(CategoryParams @params)
        {
            var result = await _service.GetCategories(@params);
            return PartialView("~/Views/Category/_CategoryList.cshtml", result.Data.CategoryList);
        }        
    }
}
