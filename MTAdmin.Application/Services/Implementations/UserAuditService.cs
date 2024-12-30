using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Helpers;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class UserAuditService : BaseService, IUserAuditService
    {
        private readonly IUnitOfWork _uof;
        public UserAuditService(IUnitOfWork uof,ICurrentUserService currentUser):base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = currentUser.GetUserId();
        }
        public async Task<Response<PagedList<UserAuditVM>>> GetUserAudits(UserAuditParameters @params)
        {
            var list = await _uof.UserAuditRepo.GetUserAudits(@params);
            if (list.IsSuccess)
            {
                foreach (var item in list.Data.Items)
                {
                    if (Enum.TryParse(item.Event, out AuditEventType result))
                    {
                        item.Event = result.GetDescription();   
                    }
                }
            }
            return list;
        }
    }
}
