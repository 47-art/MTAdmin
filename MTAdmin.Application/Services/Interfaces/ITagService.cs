using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface ITagService
    {
        Task<Response<int>> CreateTag(CreateTagVM vm);
        Task<Response<int>> DeleteTag(int id);
        Task<Response<TagVM>> GetTagById(int id);
        Task<Response<PagedList<TagPageVM>>> GetTags(TagParameters @params);
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> UpdateTag(TagVM vm);
    }
}
