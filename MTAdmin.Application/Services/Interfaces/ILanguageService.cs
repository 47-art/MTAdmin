using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<Response<int>> CreateLaguage(CreateLanguageVM vm);
        Task<Response<int>> DeleteLanguage(int id);
        Task<Response<LanguageVM>> GetLanguageById(int id);
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> UpdateLanguage(LanguageVM vm);
        Task<Response<PagedList<LanguagePageVM>>> GetLanguages(LanguageParameters @params);
    }
}
