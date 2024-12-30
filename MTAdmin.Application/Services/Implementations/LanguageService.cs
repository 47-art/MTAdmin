using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Entities.Language;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class LanguageService : BaseService, ILanguageService
    {
        private readonly IUnitOfWork _uof;
        public LanguageService(IUnitOfWork uof, ICurrentUserService currentUser) : base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = _currentUser.GetUserId();
        }
        public async Task<Response<int>> CreateLaguage(CreateLanguageVM vm) => await _uof.LanguageRepo.CreateLanguage(new Language()
        {
            Name = vm.Name,
            NativeName = vm.NativeName,
            Slug = vm.Slug,
            IsActive = vm.IsActive
        });
        public async Task<Response<LanguageVM>> GetLanguageById(int id)
        {
            var language = await _uof.LanguageRepo.GetLanguageById(id);
            if (language.IsSuccess)
            {
                LanguageVM vm = new LanguageVM()
                {
                    Id = language.Data.Id,
                    Name = language.Data.Name,
                    CreatedBy = language.Data.CreatedBy,
                    NativeName = language.Data.NativeName,
                    Slug = language.Data.Slug,
                    CreatedAt = language.Data.CreatedAt,
                    ModifiedBy = language.Data.ModifiedBy,
                    ModifiedAt = language.Data.ModifiedAt,
                    IsActive = language.Data.IsActive
                };
                return Response<LanguageVM>.Success(vm);
            }
            else
            {
                return Response<LanguageVM>.Error(language.Ex, message: language.Message);
            }
        }
        public async Task<Response<int>> DeleteLanguage(int id) => await _uof.LanguageRepo.DeleteLanguage(id);
        public async Task<Response<bool>> ActiveInActive(int id) => await _uof.LanguageRepo.ActiveInActive(id);
        public async Task<Response<int>> UpdateLanguage(LanguageVM vm) => await _uof.LanguageRepo.UpdateLanguage(new Language()
        {
            Id = vm.Id,
            Name = vm.Name,
            NativeName = vm.NativeName,
            Slug = vm.Slug,
            IsActive = vm.IsActive
        });
        public async Task<Response<PagedList<LanguagePageVM>>> GetLanguages(LanguageParameters @params) => await _uof.LanguageRepo.GetLanguages(@params);
    }
}
