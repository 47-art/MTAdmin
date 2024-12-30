using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service, IToastNotification notification) : base(notification) => _service = service;
        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            var parameters = new CategoryParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetCategory(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
        public IActionResult Add()
        {
            return View(new CreateCategoryVM() { IsActive = true });
        }

        [HttpPost]
        [RequestSizeLimit(24 * 1024 * 1024)]
        public async Task<IActionResult> Add(CreateCategoryVM vm)
        {
            if (ModelState.IsValid)
            {               
                var result = await _service.CreateCategory(vm);
                ShowMessage(result);
                if (result.IsSuccess && result.Data > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(vm);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _service.GetCategoryById(Id);
            ShowMessage(result);
            if (result.IsSuccess)
            {                
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateCategory(vm);
                ShowMessage(result);
                if (result.IsSuccess && result.Data > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Ex.Message);
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _service.DeleteCategory(Id);
            ShowMessage(result);
            return Json(result.Data);
        }

        [HttpPost]
        [RequestSizeLimit(24 * 1024 * 1024)]
        public async Task<IActionResult> EditCategoryImg([FromForm] EditCategoryImgVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateCategoryImg(vm);
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
    }
}