using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.Language
{
    public interface ILanguageRepo
    {
        string CurrentUserId { get; set; }
        Task<Response<int>> CreateLanguage(Language language);
        Task<Response<int>> DeleteLanguage(int id);
        Task<Response<Language>> GetLanguageById(int id);
        Task<Response<PagedList<LanguagePageVM>>> GetLanguages(LanguageParameters @params);
        Task<Response<IReadOnlyList<IdNameVM<int>>>> GetLanguagesDropdownValues();
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> UpdateLanguage(Language language);
    }
}
