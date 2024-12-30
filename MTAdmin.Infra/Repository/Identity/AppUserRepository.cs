using Microsoft.EntityFrameworkCore;
using MTAdmin.Core.Entities.UserAudit;
using MTAdmin.Core.Interfaces;
using MTAdmin.Infra.Data;
using MTAdmin.Infra.Identity;
using MTAdmin.Shared.Models;

namespace MTAdmin.Infra.Repository.Identity
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IdentityContext _identityContext;
        public AppUserRepository(AppDbContext context,IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }
        public async Task<int> SaveUserAudit(UserAuditModel model)
        {
            UserAudit audit = new UserAudit()
            {
                Event = model.Event,
                IPAddress = model.IPAddress,
                UserId = model.UserId
            };
            _context.Entry(audit).State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<UserIdAndNameModel>> GetUserNames(string[] userIds)
        {
           return await _identityContext.AppUsers.Where(x => userIds.Contains(x.Id)).Select(x => new UserIdAndNameModel()
            {
                UserId = x.Id,
                UserName = x.UserName
            }).ToListAsync();
        }
    }
}
