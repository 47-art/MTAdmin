using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class TemplateController : BaseController
    {
        private readonly ITemplateService _service;

        public TemplateController(ITemplateService service, IToastNotification notification) : base(notification) => _service = service;

        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            var parameters = new TemplateParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetTemplates(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
        public async Task<IActionResult> Add()
        {            
            await SetViewBags();
            return View(new CreateTemplateVM() { IsActive = false });
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateTemplateVM vm)
        {
            if (ModelState.IsValid)
            {                                
                var result = await _service.AddTemplate(vm);
                ShowMessage(result);
                if (result.IsSuccess)
                {                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await SetViewBags();
                }
            }
            else
            {
                await SetViewBags();
            }
            return View(vm);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _service.GetTemplateById(Id);
            await SetViewBags();
            ShowMessage(result);
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTemplateVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.EditTemplate(vm);
                ShowMessage(result);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await SetViewBags();
                }
            }
            else
            {
                await SetViewBags();
            }
            return View(vm);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _service.DeleteTemplate(Id);
            ShowMessage(result);
            return RedirectToAction(nameof(Index));
        }        
        public async Task<IActionResult> EditTemplateThumbnail(int Id)
        {
            var result = await _service.GetTemplateThumbnail(Id);
            ShowMessage(result);
            return PartialView("~/Views/Template/_ThumbnailEdit.cshtml", result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> EditTemplateThumbnail(EditTemplateThumbnailVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateThumbnailFileName(vm);
                ShowMessage(response);
                return Json(response);
            }
            else
            {
                return Json(Response<int>.Error(null, message: "Model state is not valid"));
            }
        }
        public async Task<IActionResult> EditTemplateFile(int Id)
        {
            var result = await _service.GetTemplateFile(Id);
            ShowMessage(result);
            return PartialView("~/Views/Template/_TemplateEdit.cshtml", result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> EditTemplateFile(EditTemplateFileVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateTemplateFileName(vm);
                ShowMessage(response);
                return Json(response);
            }
            else
            {
                return Json(Response<int>.Error(null, message: "Model state is not valid"));
            }
        }
        public async Task<IActionResult> ActiveInActive(int Id)
        {
            var result = await _service.ActiveInActive(Id);
            ShowMessage(result);
            return RedirectToAction(nameof(Index));
        }
        private async Task SetViewBags()
        {
            var languages = await _service.GetLanguagesForDropdown();
            var categories = await _service.GetCategoriesForDropdown();
            var tags = await _service.GetTagsForDropdown();
            if (languages.IsSuccess)
            {
                ViewBag.Languages = languages.Data.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            if (categories.IsSuccess)
            {
                ViewBag.Categories = categories.Data.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            if (tags.IsSuccess)
            {
                ViewBag.Tags = tags.Data.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
        }
    }
}
