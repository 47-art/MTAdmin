using Microsoft.AspNetCore.StaticFiles;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Helpers;

namespace MTAdmin.Web.Areas.Archone.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private const int FullScreenWidth = 1000;
        public FileService(IWebHostEnvironment webHostEnv)
        {            
            _webHostEnv = webHostEnv;
        }
        public async Task<string> ProcessAndSaveAsync(Stream stream)
        {
            using (var image = await Image.LoadAsync(stream))
            {                
                int height = image.Height;
                int width = image.Width;

                if (width > FullScreenWidth)
                {
                    height = (int)(double)FullScreenWidth / width * height;
                    width = FullScreenWidth;
                }

                image.Mutate(x => x.Resize(new Size(width, height)));
                image.Metadata.ExifProfile = null;

                var file = GetFilePath();
                await image.SaveAsJpegAsync(file.path, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                {
                    Quality = 75,
                });
                return file.fileName;
            }
        }
        public string GetFileUrl(string fileName) => $"/siteimg/{fileName}";
        private string RandomName(string ext, string preFix = "file") => $"{preFix}_{Guid.NewGuid()}.{ext}";
        private (string fileName, string path) GetFilePath()
        {
            string path = GetSiteImgPath();
            Directory.CreateDirectory(path);
            string fileName = RandomName("jpg");
            path = Path.Combine(path, fileName);
            return (fileName: fileName, path: path);
        }
        private string GetSiteImgPath()
        {
           //string[] list = Directory.GetDirectories(_webHostEnv.WebRootPath).Where(x => x.StartsWith("f_")).ToArray();
           //string path = string.Empty;
           //int listLength = list.Length;
           //int i = 0;

           //if(listLength > 0)
           //{
           //     while (i == listLength)
           //     {
           //         if (Directory.GetFiles(Path.Combine(_webHostEnv.WebRootPath, list[i]),"*").Length <= 1000)
           //         {
           //             path = Path.Combine(_webHostEnv.WebRootPath, list[i]);
           //             break;
           //         }
           //         i++;
           //     }
           // }            

           // if(path == string.Empty)
           // {
           //     path = Path.Combine(_webHostEnv.WebRootPath, $"f_{Guid.NewGuid()}");
           // }

            return Path.Combine(_webHostEnv.WebRootPath, "siteimg");
        }
        public void DeleteFile(string fileName)
        {
            if (fileName.Contains("/siteimg/",StringComparison.InvariantCulture))
            {
                fileName = fileName.TextAfter("/siteimg/");
            }
            string path = Path.Combine(GetSiteImgPath(), fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            string fileName = Path.GetFileName(file.FileName);
            string extension = Path.GetExtension(fileName);
            string newFileName = string.Concat(Guid.NewGuid(), extension);
            string path = Path.Combine(GetSiteImgPath(), newFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            if (File.Exists(path))
            {
                DateTime now = DateTime.Now;
                File.SetCreationTimeUtc(path, now);
                File.SetLastAccessTimeUtc(path, now);
                File.SetLastWriteTimeUtc(path, now);                
            }
            return newFileName;
        }
        public string GetFileContentType(string fileName)
        {
           new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType);
           return contentType;
        }
        public TemplateTypeEnum GetTemplateType(string fileName)
        {
            string contentType = GetFileContentType(fileName);
            if (contentType.StartsWith("video"))
                return TemplateTypeEnum.Video;
            else if (contentType.StartsWith("image"))
                return TemplateTypeEnum.Image;
            else
                return TemplateTypeEnum.Undefined;
        }
    }
}
