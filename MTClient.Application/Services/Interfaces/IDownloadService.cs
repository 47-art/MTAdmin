using MTAdmin.Shared.Models;

namespace MTClient.Application.Services.Interfaces
{
    public interface IDownloadService
    {
        Task<Response<string>> GetTemplateFilePath(int id);
    }
}
