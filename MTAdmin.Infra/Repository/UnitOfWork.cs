using MTAdmin.Core.Entities.Category;
using MTAdmin.Core.Entities.Contact;
using MTAdmin.Core.Entities.EmailSubscriber;
using MTAdmin.Core.Entities.Language;
using MTAdmin.Core.Entities.Tag;
using MTAdmin.Core.Entities.Template;
using MTAdmin.Core.Entities.UserAudit;
using MTAdmin.Core.Interfaces;
using MTAdmin.Infra.Dapper;

namespace MTAdmin.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public string CurrentUserId { get; set; }
        private readonly DapperContext _context;
        private ICategoryRepo _categoryRepo;
        private IContactRepo _contactRepo;
        private IEmailSubscriberRepo _emailSubscriberRepo;
        private ILanguageRepo _languageRepo;
        private ITagRepo _tagRepo;
        private ITemplateRepo _templateRepo;
        private IUserAuditRepo _userAuditRepo;
        public UnitOfWork(DapperContext context)
        {
            _context = context;
        }
        public ICategoryRepo CategoryRepo
        {
            get { return _categoryRepo ?? (_categoryRepo = new CategoryRepo(_context, CurrentUserId)); }
        }

        public IContactRepo ContactRepo
        {
            get { return _contactRepo ?? (_contactRepo = new ContactRepo(_context)); }
        }

        public IEmailSubscriberRepo EmailSubscriberRepo
        {
            get { return _emailSubscriberRepo ?? (_emailSubscriberRepo = new EmailSubscriberRepo(_context)); }
        }

        public ILanguageRepo LanguageRepo
        {
            get { return _languageRepo ?? (_languageRepo = new LanguageRepository(_context, CurrentUserId)); }
        }

        public ITagRepo TagRepo
        {
            get { return _tagRepo ?? (_tagRepo = new TagRepo(_context,CurrentUserId)); }
        }

        public ITemplateRepo TemplateRepo
        {
            get { return _templateRepo ?? (_templateRepo = new TemplateRepo(_context, CurrentUserId)); }
        }
        public IUserAuditRepo UserAuditRepo
        {
            get { return _userAuditRepo ?? (_userAuditRepo = new UserAuditRepo(_context, CurrentUserId)); }
        }
        public void Dispose()
        {

        }
    }
}
