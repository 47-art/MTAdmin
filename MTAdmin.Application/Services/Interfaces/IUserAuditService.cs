using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface IUserAuditService
    {
        Task<Response<PagedList<UserAuditVM>>> GetUserAudits(UserAuditParameters @params);
    }
}
