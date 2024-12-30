using MTAdmin.Application.Services.Interfaces;

namespace MTAdmin.Application.Services.Comman
{
    public class BaseService
    {
        internal readonly ICurrentUserService _currentUser;
        public BaseService(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }
    }
}
