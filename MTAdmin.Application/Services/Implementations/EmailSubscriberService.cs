using MTAdmin.Application.Services.Comman;
using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Interfaces;

namespace MTAdmin.Application.Services.Implementations
{
    public class EmailSubscriberService : BaseService, IEmailSubscriberService
    {
        private readonly IUnitOfWork _uof;
        public EmailSubscriberService(IUnitOfWork uof, ICurrentUserService currentUser) : base(currentUser)
        {
            _uof = uof;
            _uof.CurrentUserId = _currentUser.GetUserId();
        }
    }
}
