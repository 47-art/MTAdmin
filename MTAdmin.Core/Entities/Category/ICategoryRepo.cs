using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.Category
{
    public interface ICategoryRepo
    {
        public string CurrentUserId { get; set; }
        Task<Response<int>> CreateCategory(CreateCategoryVM category);
        Task<Response<int>> DeleteCategory(int id);
        Task<Response<PagedList<CategoryPageVM>>> GetCategories(CategoryParameters @params);
        Task<Response<IReadOnlyList<IdNameVM<int>>>> GetCategoriesDropDownValues();
        Task<Response<CategoryVM>> GetCategoryById(int id);
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> UpdateCategory(CategoryVM category);
        Task<Response<int>> UpdateCategoryImg(int Id, string filePath);
    }
}
