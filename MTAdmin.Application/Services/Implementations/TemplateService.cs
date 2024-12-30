using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Helpers;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class TemplateService : BaseService, ITemplateService
    {
        private readonly IUnitOfWork _uof;
        private readonly IFileService _fileService;
        public TemplateService(IUnitOfWork uof, ICurrentUserService currentUser,
            IFileService fileService) : base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = _currentUser.GetUserId();
            _fileService = fileService;
        }
        public async Task<Response<PagedList<TemplatePageVM>>> GetTemplates(TemplateParameters @params) => await _uof.TemplateRepo.GetTemplates(@params);
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetLanguagesForDropdown() => await _uof.LanguageRepo.GetLanguagesDropdownValues();
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetCategoriesForDropdown() => await _uof.CategoryRepo.GetCategoriesDropDownValues();
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetTagsForDropdown() => await _uof.TagRepo.GetTagsDropdownValues();
        public async Task<Response<int>> AddTemplate(CreateTemplateVM vm)
        {
            if (vm.ThumbnailFile is not null && vm.ThumbnailFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await vm.ThumbnailFile.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    vm.Thumbnail = await _fileService.ProcessAndSaveAsync(ms);
                }
            }

            if (vm.TemplateFile is not null && vm.TemplateFile.Length > 0)
            {
                vm.FileName = await _fileService.UploadFile(vm.TemplateFile);
            }

            return await _uof.TemplateRepo.AddTemplate(vm);
        }
        public async Task<Response<int>> EditTemplate(EditTemplateVM vm) => await _uof.TemplateRepo.EditTemplate(vm);
        public async Task<Response<EditTemplateVM>> GetTemplateById(int Id)
        {
            var result = await _uof.TemplateRepo.GetTemplateById(Id);
            if (result.IsSuccess)
            {                          
                result.Data.CategoryIds = result.Data.Categories?.Split(',')?.Select(Int32.Parse)?.ToList();
                result.Data.TagIds = result.Data.Tags?.Split(',')?.Select(Int32.Parse)?.ToList();
            }            
            return result;
        }
        public async Task<Response<int>> DeleteTemplate(int id) => await _uof.TemplateRepo.DeleteTemplate(id);
        public async Task<Response<int>> UpdateThumbnailFileName(EditTemplateThumbnailVM vm)
        {
            var result = await GetTemplateById(vm.Id);
            if (result.IsSuccess)
            {
                if (vm.ThumbnailFile is not null && vm.ThumbnailFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await vm.ThumbnailFile.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        vm.Thumbnail = await _fileService.ProcessAndSaveAsync(ms);
                    }                    
                    _fileService.DeleteFile(result.Data.Thumbnail);                    
                }
                if (string.IsNullOrWhiteSpace(vm.Thumbnail))
                {
                    vm.Thumbnail = result.Data.Thumbnail.TextAfter("/siteimg/");
                }
                return await _uof.TemplateRepo.UpdateThumbnailFileName(vm);
            }
            else
            {
                return Response<int>.Error(result.Ex, 0, message: result.Message);
            }
        }
        public async Task<Response<int>> UpdateTemplateFileName(EditTemplateFileVM vm)
        {
            var result = await GetTemplateById(vm.Id);
            Response<int> response;
            if (result.IsSuccess)
            {
                if (vm.TemplateFile is not null && vm.TemplateFile.Length > 0)
                {
                    vm.FileName = await _fileService.UploadFile(vm.TemplateFile);
                    var updateTemplateResult = await _uof.TemplateRepo.UpdateTemplateFileName(vm);
                    _fileService.DeleteFile(result.Data.FileName);
                    response = updateTemplateResult;
                }
                else
                {
                    response = Response<int>.Error(result.Ex, 0, message: "File is null");
                }
            }
            else
            {
                response = Response<int>.Error(result.Ex, 0, message: result.Message);
            }
            return response;
        }
        public async Task<Response<EditTemplateThumbnailVM>> GetTemplateThumbnail(int Id)
        {
            var result = await _uof.TemplateRepo.GetTemplateThumbnail(Id);
            if (result.IsSuccess)
            {
                result.Data.Thumbnail = _fileService.GetFileUrl(result.Data.Thumbnail);
            }
            return result;
        }
        public async Task<Response<EditTemplateFileVM>> GetTemplateFile(int Id)
        {
            var result = await _uof.TemplateRepo.GetTemplateFile(Id);
            if (result.IsSuccess)
            {
                result.Data.FileName = _fileService.GetFileUrl(result.Data.FileName);
                result.Data.TemplateType = _fileService.GetTemplateType(result.Data.FileName);
            }
            return result;
        }
        public async Task<Response<bool>> ActiveInActive(int id) => await _uof.TemplateRepo.ActiveInActive(id);
    }
}
