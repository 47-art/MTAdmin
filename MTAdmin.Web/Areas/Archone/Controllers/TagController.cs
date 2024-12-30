using Microsoft.AspNetCore.Mvc;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.ViewModels;
using NToastNotify;

namespace MTAdmin.Web.Areas.Archone.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagService _service;

        public TagController(ITagService service, IToastNotification notification) : base(notification) => _service = service;
        public async Task<IActionResult> Index(string s, int page = 1)
        {
            ViewData["s"] = s;
            var parameters = new TagParameters()
            {
                Query = s,
                PageNumber = page
            };
            var result = await _service.GetTags(parameters);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        public IActionResult Add()
        {
            return View(new CreateTagVM() { IsActive = true });
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateTagVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateTag(vm);
                ShowMessage(result);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _service.GetTagById(Id);
            ShowMessage(result);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateTag(vm);
                ShowMessage(result);
                if (result.IsSuccess && result.Data > 0)
                {
                    //_toastNotification.AddSuccessToastMessage("Tag Edited");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Ex.Message);
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _service.DeleteTag(0);
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
