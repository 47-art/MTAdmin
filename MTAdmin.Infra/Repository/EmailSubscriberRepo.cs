using MTAdmin.Core.Entities.EmailSubscriber;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;

namespace MTAdmin.Infra.Repository
{
    public class EmailSubscriberRepo : BaseRepository, IEmailSubscriberRepo
    {
        public EmailSubscriberRepo(DapperContext context) : base(context) { }
    }
}
