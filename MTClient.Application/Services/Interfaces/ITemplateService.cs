using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using MTAdmin.Shared.ViewModels.Client;

namespace MTClient.Application.Services.Interfaces
{
    public interface ITemplateService
    {
        Task<Response<IReadOnlyList<IdNameVM<int>>>> GetCategories();
        Task<Response<MetaDataModel>> GetHomePageMetaData();
        Task<Response<MetaDataModel>> GetSearchPageMetaData();
        Task<Response<TemplateByIdVM>> GetTemplateById(int Id);
        Task<Response<ListTemplateVM>> GetTemplates(TemplateParams @params);
        Task<Response<int>> SharePostStatestic(ShareTemplateVM vm);
    }
}
