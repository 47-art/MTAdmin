using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.Template
{
    public interface ITemplateRepo
    {
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> AddTemplate(CreateTemplateVM vm);
        Task<Response<int>> DeleteTemplate(int id);
        Task<Response<int>> EditTemplate(EditTemplateVM vm);
        Task<Response<EditTemplateVM>> GetTemplateById(int Id);
        Task<Response<EditTemplateFileVM>> GetTemplateFile(int Id);
        Task<Response<PagedList<TemplatePageVM>>> GetTemplates(TemplateParameters @params);
        Task<Response<EditTemplateThumbnailVM>> GetTemplateThumbnail(int Id);
        Task<Response<int>> UpdateTemplateFileName(EditTemplateFileVM vm);
        Task<Response<int>> UpdateThumbnailFileName(EditTemplateThumbnailVM vm);
    }
}
