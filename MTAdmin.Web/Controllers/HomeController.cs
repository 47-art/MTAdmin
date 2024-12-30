using Microsoft.AspNetCore.Mvc;
using MTClient.Application.Services.Interfaces;
using MTAdmin.Shared.ViewModels.Client;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.Controllers
{
    public class HomeController : ClientBaseController
    {
        private readonly ITemplateService _service;        
        public HomeController(ITemplateService service) { _service = service;}
        [Route("/{category?}/{filter?}/{slug?}")]
        public async Task<IActionResult> Index(string category=null,string slug=null,string filter=null)
        {
            var @params = new TemplateParams()
            {
                Query = null,
                CategoryId = 0,
                FilterBy = FilterByEnum.latest
            };
            if (!string.IsNullOrWhiteSpace(category) && int.TryParse(category, out int categoryId))
            {
                @params.CategoryId = categoryId;
            }
            if (!string.IsNullOrWhiteSpace(filter) && Enum.TryParse(filter, out FilterByEnum filterBy))
            {
                if (Enum.IsDefined(filterBy))
                {
                    @params.FilterBy = filterBy;
                }
            }           
            var result = await _service.GetTemplates(@params);
            var metaTags = await _service.GetHomePageMetaData();
            if (result.IsSuccess)
            {
                result.Data.CategorySlug = slug;
                int i = 0;
                while(i < result.Data.TemplateList.Items.Count)
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
        [Route("/{id}/{slug}")]
        public async Task<IActionResult> Detail(string id,string slug)
        {
            TemplateByIdVM vm = new TemplateByIdVM();
            if(int.TryParse(id,out int Id))
            {
                var result = await _service.GetTemplateById(Id);
                if (result.IsSuccess)
                {
                    vm = result.Data;
                    vm.TemplateType = GetFileContentType(vm.FileName);
                    if (!slug.Equals(vm.Slug))
                    {
                        return RedirectToAction(nameof(Detail), new { id = vm.Id, slug = vm.Slug });
                    }
                    AddMetaData(new MetaDataModel()
                    {
                        MetaTitle = vm.Name,
                        MetaDescription = vm.MetaDesc,
                        MetaKeywords = string.Concat(vm.MetaKeywords, vm.CategoryMetaKeywords)
                    });
                }
            }            
            return View(vm);
        }        
        [HttpPost]
        [Route("gettemplates")]
        public async Task<IActionResult> GetTemplates(TemplateParams @params)
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
            return PartialView("~/Views/Home/_TemplateListPartial.cshtml", result.Data.TemplateList);
        }
        [Route("GetCategoriesDD")]
        public async Task<IActionResult> GetCategoriesDD()
        {
            var result = await _service.GetCategories();
            return Json(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> ShareTemplate(ShareTemplateVM vm)
        {
            if (Enum.IsDefined(vm.ShareType))
            {
                var result = await _service.SharePostStatestic(vm);
                if(result.IsSuccess && result.Data > 0)
                {
                    return Json(result);
                }
                else
                {
                    return Json(Response<int>.Success(1, ""));
                }
            }
            else
            {
                return Json(Response<int>.Success(0, "Invalid Share"));
            }            
        }        
    }
}