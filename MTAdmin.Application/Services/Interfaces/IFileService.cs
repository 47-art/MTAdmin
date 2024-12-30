using Microsoft.AspNetCore.Http;
using MTAdmin.Shared.Enums;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface IFileService
    {
        void DeleteFile(string fileName);
        string GetFileContentType(string fileName);
        string GetFileUrl(string fileName);
        TemplateTypeEnum GetTemplateType(string fileName);
        Task<string> ProcessAndSaveAsync(Stream stream);
        Task<string> UploadFile(IFormFile file);
    }
}
