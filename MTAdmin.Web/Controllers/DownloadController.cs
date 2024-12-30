using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MTClient.Application.Services.Interfaces;

namespace MTAdmin.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IDownloadService _service;
        public DownloadController(IWebHostEnvironment webHostEnv,IDownloadService service)
        {
            _webHostEnv = webHostEnv;
            _service = service;
        }
        [Route("download/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var response = await _service.GetTemplateFilePath(id);
            if (response.IsSuccess)
            {
                string path = Path.Combine(_webHostEnv.WebRootPath, response.Data);
               if (System.IO.File.Exists(path))
               {
                    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = string.Format("Template.{0}", Path.GetExtension(path)),
                        Inline = false,
                    };
                    Response.Headers.Add("Content-Disposition", cd.ToString());
                    Response.Headers.Add("X-Content-Type-Options", "nosmiff");
                    new FileExtensionContentTypeProvider().TryGetContentType(cd.FileName, out string contentType);
                    return File(await System.IO.File.ReadAllBytesAsync(path),contentType,cd.FileName);
               }
            }
            return View();
        }
    }
}
