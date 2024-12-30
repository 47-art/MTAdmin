using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Entities.Tag;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class TagService : BaseService, ITagService
    {
        private readonly IUnitOfWork _uof;
        public TagService(IUnitOfWork uof, ICurrentUserService currentUser) : base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = _currentUser.GetUserId();
        }
        public async Task<Response<int>> CreateTag(CreateTagVM vm) => await _uof.TagRepo.CreateTag(new Tag()
        {
            Name = vm.Name,
            Slug = vm.Slug,
            IsActive = vm.IsActive,
        });
        public async Task<Response<TagVM>> GetTagById(int id)
        {
            var tag = await _uof.TagRepo.GetTagById(id);
            if (tag.IsSuccess)
            {
                TagVM vm = new TagVM()
                {
                    Id = tag.Data.Id,
                    Name = tag.Data.Name,
                    CreatedBy = tag.Data.CreatedBy,
                    IsActive = tag.Data.IsActive,
                    Slug = tag.Data.Slug,
                    CreatedAt = tag.Data.CreatedAt,
                    ModifiedBy = tag.Data.ModifiedBy,
                    ModifiedAt = tag.Data.ModifiedAt
                };
                return Response<TagVM>.Success(vm);
            }
            else
            {
                return Response<TagVM>.Error(tag.Ex, message: tag.Message);
            }
        }
        public async Task<Response<int>> DeleteTag(int id) => await _uof.TagRepo.DeleteTag(id);
        public async Task<Response<bool>> ActiveInActive(int id) => await _uof.TagRepo.ActiveInActive(id);
        public async Task<Response<int>> UpdateTag(TagVM vm) => await _uof.TagRepo.UpdateTag(new Tag()
        {
            Id = vm.Id,
            Name = vm.Name,
            Slug = vm.Slug,
            IsActive = vm.IsActive,
        });
        public async Task<Response<PagedList<TagPageVM>>> GetTags(TagParameters @params) => await _uof.TagRepo.GetTags(@params);
    }
}
