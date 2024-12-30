using MTAdmin.Application.Services.Interfaces;
using System.Security.Claims;

namespace MTAdmin.Web.Areas.Archone.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpContext context;
        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            if (_accessor.HttpContext is not null)
            {
                context = _accessor.HttpContext;
            }
        }
        public string GetUserId()
        {
            if (context != null && context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId is not null)
                    return userId.Value;
            }
            return string.Empty;
        }
        public string GetUserEmail()
        {
            if (context != null && context.User.HasClaim(x => x.Type == ClaimTypes.Email))
            {
                Claim email = context.User.FindFirst(ClaimTypes.Email);
                if (email is not null)
                    return email.Value;
            }
            return string.Empty;
        }

        public string GetFullName()
        {
            if (context != null)
            {
                string fullName = string.Empty;
                Claim firstName = context.User.FindFirst(ClaimTypes.GivenName);
                Claim lastName = context.User.FindFirst(ClaimTypes.Surname);
                if (firstName is not null)
                    fullName = firstName.Value;
                if (lastName is not null)
                    fullName = $"{fullName} {lastName.Value}";
                return fullName;
            }
            return string.Empty;
        }

        public string GetRoles()
        {
            if (context is not null && context.User.HasClaim(x => x.Type == ClaimTypes.Role))
            {
                Claim roles = context.User.FindFirst(ClaimTypes.Role);
                if (roles is not null)
                    return roles.Value;
            }
            return string.Empty;
        }

        public bool IsUserAuthenticated()
        {
            if (context is not null && context.User.Identity is not null)
                return context.User.Identity.IsAuthenticated;

            return false;
        }
        public string GetIPAddress()
        {
            return _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
