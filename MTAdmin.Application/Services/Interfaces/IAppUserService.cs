using MTAdmin.Shared.Models;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<IReadOnlyList<UserIdAndNameModel>> GetUserNames(string[] userIds);
        Task<int> SaveUserAudit(UserAuditModel model);
    }
}
