using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _service;
        public LanguageController(ILanguageService service, IToastNotification notification) : base(notification)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            
            var parameters = new LanguageParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetLanguages(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
        public IActionResult Add()
        {            
            return View(new CreateLanguageVM());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateLanguageVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateLaguage(vm);
                ShowMessage(result);
                if (result.IsSuccess && result.Data > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View();
        }        

        public async Task<IActionResult> Edit(int Id)
        {            
            var result = await _service.GetLanguageById(Id);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LanguageVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateLanguage(vm);
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
            var result = await _service.DeleteLanguage(Id);
            ShowMessage(result);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ActiveInActive(int Id)
        {
            var result = await _service.ActiveInActive(Id);
            ShowMessage(result);
            return RedirectToAction(nameof(Index));
        }
    }
}
