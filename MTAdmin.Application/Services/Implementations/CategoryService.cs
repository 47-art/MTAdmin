using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Entities.Category;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWork _uof;
        private readonly IFileService _fileService;
        public CategoryService(IUnitOfWork uof, ICurrentUserService currentUser,IFileService fileService) : base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = _currentUser.GetUserId();
            _fileService = fileService;
        }
        public async Task<Response<int>> CreateCategory(CreateCategoryVM vm)
        {
            if (vm.ImgFile is not null && vm.ImgFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await vm.ImgFile.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    vm.Img = await _fileService.ProcessAndSaveAsync(ms);
                }
            }
            return await _uof.CategoryRepo.CreateCategory(vm);
        }
        public async Task<Response<CategoryVM>> GetCategoryById(int id)
        {
            var category = await _uof.CategoryRepo.GetCategoryById(id);
            if (category.IsSuccess && category.Data != null)
                category.Data.Img = _fileService.GetFileUrl(category.Data.Img);
            return category;
        }
        public async Task<Response<int>> DeleteCategory(int id) => await _uof.CategoryRepo.DeleteCategory(id);
        public async Task<Response<bool>> ActiveInActive(int id) => await _uof.CategoryRepo.ActiveInActive(id);
        public async Task<Response<int>> UpdateCategory(CategoryVM vm) => await _uof.CategoryRepo.UpdateCategory(vm);
        public async Task<Response<PagedList<CategoryPageVM>>> GetCategory(CategoryParameters @params) => await _uof.CategoryRepo.GetCategories(@params);
        public async Task<Response<int>> UpdateCategoryImg(EditCategoryImgVM vm)
        {
            var result = await GetCategoryById(vm.Id);
            if (result.IsSuccess)
            {
                string fileName = string.Empty;
                if (vm.Img is not null && vm.Img.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await vm.Img.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        fileName = await _fileService.ProcessAndSaveAsync(ms);
                    }
                    _fileService.DeleteFile(result.Data.Img);
                }
                return await _uof.CategoryRepo.UpdateCategoryImg(vm.Id, fileName);
            }
            else
            {
                return Response<int>.Error(result.Ex, 0, result.Message);
            }
        }
    }
}
