using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Models;

namespace MTAdmin.Application.Services.Implementations
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _repository;
        public AppUserService(IAppUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> SaveUserAudit(UserAuditModel model)
        {
            return await _repository.SaveUserAudit(model);
        }
        public async Task<IReadOnlyList<UserIdAndNameModel>> GetUserNames(string[] userIds)
        {
            return await _repository.GetUserNames(userIds);
        }
    }
}
