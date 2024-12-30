using MTAdmin.Shared.Models;

namespace MTAdmin.Core.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IReadOnlyList<UserIdAndNameModel>> GetUserNames(string[] userIds);
        Task<int> SaveUserAudit(UserAuditModel model);
    }
}
