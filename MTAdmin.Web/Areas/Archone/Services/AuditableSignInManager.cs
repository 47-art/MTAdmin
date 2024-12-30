using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MTAdmin.Application.Services.Interfaces;

namespace MTAdmin.Web.Areas.Archone.Services
{
    public class AuditableSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAppUserService _appUserService;

        public AuditableSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, IAppUserService appUserService)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, null, null)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _appUserService = appUserService;
        }

        public override async Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            var appUser = user as IdentityUser;

            if (appUser != null) // We can only log an audit record if we can access the user object and it's ID
            {
                //string ip = GetIPAddress();

                //UserAuditModel auditRecord = null;

                //switch (result.ToString())
                //{
                //    case "Succeeded":
                //        auditRecord = new UserAuditModel() { UserId = appUser.Id, IPAddress = ip, Event = AuditEventType.Login };
                //        break;

                //    case "Failed":
                //        auditRecord = new UserAuditModel() { UserId = appUser.Id, IPAddress = ip, Event = AuditEventType.FailedLogin };
                //        break;
                //}

                //if (auditRecord != null)
                //{
                //   await _appUserService.SaveUserAudit(auditRecord);
                //}
            }

            return result;
        }

        public override async Task SignOutAsync()
        {
            await base.SignOutAsync();

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(_contextAccessor.HttpContext.User)) as IdentityUser;

            if (user != null)
            {
                //await _appUserService.SaveUserAudit(new UserAuditModel()
                //{
                //    UserId = user.Id,
                //    Event = AuditEventType.LogOut,
                //    IPAddress = GetIPAddress()
                //});
            }
        }
        private string GetIPAddress()
        {
            return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
