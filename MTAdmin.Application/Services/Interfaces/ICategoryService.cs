using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<int>> CreateCategory(CreateCategoryVM vm);
        Task<Response<int>> DeleteCategory(int id);
        Task<Response<PagedList<CategoryPageVM>>> GetCategory(CategoryParameters @params);
        Task<Response<CategoryVM>> GetCategoryById(int id);
        Task<Response<int>> UpdateCategory(CategoryVM vm);
        Task<Response<int>> UpdateCategoryImg(EditCategoryImgVM vm);
        Task<Response<bool>> ActiveInActive(int id);
    }
}
