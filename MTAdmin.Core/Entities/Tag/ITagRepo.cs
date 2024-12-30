using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.Tag
{
    public interface ITagRepo
    {
        string CurrentUserId { get; set; }
        Task<Response<int>> CreateTag(Tag tag); 
        Task<Response<int>> DeleteTag(int id);
        Task<Response<Tag>> GetTagById(int id);
        Task<Response<PagedList<TagPageVM>>> GetTags(TagParameters @params);
        Task<Response<IReadOnlyList<IdNameVM<int>>>> GetTagsDropdownValues();
        Task<Response<bool>> ActiveInActive(int id);
        Task<Response<int>> UpdateTag(Tag tag);
    }
}
