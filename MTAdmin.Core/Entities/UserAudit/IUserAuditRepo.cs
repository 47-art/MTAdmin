using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.UserAudit
{
    public interface IUserAuditRepo
    {
        Task<Response<PagedList<UserAuditVM>>> GetUserAudits(UserAuditParameters @params);
    }
}
