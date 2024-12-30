using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels.Client;

namespace MTClient.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<ListCategoryVM>> GetCategories(CategoryParams @params);
        Task<Response<MetaDataModel>> GetCategoryMetaData();
        Task<Response<MetaDataModel>> GetCategoryMetaDataById(int id);
        Task<Response<int>> SaveCatogyPostViewState(int catId);
    }
}
