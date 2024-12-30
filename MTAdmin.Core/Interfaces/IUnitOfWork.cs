using MTAdmin.Core.Entities.Category;
using MTAdmin.Core.Entities.Contact;
using MTAdmin.Core.Entities.EmailSubscriber;
using MTAdmin.Core.Entities.Language;
using MTAdmin.Core.Entities.Tag;
using MTAdmin.Core.Entities.Template;
using MTAdmin.Core.Entities.UserAudit;

namespace MTAdmin.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public string CurrentUserId { get; set; }
        ICategoryRepo CategoryRepo { get; }
        IContactRepo ContactRepo { get; }
        IEmailSubscriberRepo EmailSubscriberRepo { get; }
        ILanguageRepo LanguageRepo { get; }
        ITagRepo TagRepo { get; }
        ITemplateRepo TemplateRepo { get; }
        IUserAuditRepo UserAuditRepo { get; }
    }
}
