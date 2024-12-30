using Microsoft.EntityFrameworkCore;
using MTAdmin.Core.Entities.Category;
using MTAdmin.Core.Entities.Contact;
using MTAdmin.Core.Entities.Country;
using MTAdmin.Core.Entities.EmailSubscriber;
using MTAdmin.Core.Entities.GeoIpRange;
using MTAdmin.Core.Entities.Language;
using MTAdmin.Core.Entities.PageMetaTags;
using MTAdmin.Core.Entities.PostStatestic;
using MTAdmin.Core.Entities.SiteRequest;
using MTAdmin.Core.Entities.Tag;
using MTAdmin.Core.Entities.Template;
using MTAdmin.Core.Entities.TemplateCategory;
using MTAdmin.Core.Entities.TemplateTag;
using MTAdmin.Core.Entities.UserAudit;
using MTAdmin.Infra.Data.Configs;
using MTAdmin.Infra.Identity.Configs;

namespace MTAdmin.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Template> Templates => Set<Template>();
        public DbSet<TemplateCategory> TemplateCategories => Set<TemplateCategory>();
        public DbSet<TemplateTag> TemplateTags => Set<TemplateTag>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<EmailSubscriber> EmailSubscribers => Set<EmailSubscriber>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<UserAudit> UserAudits => Set<UserAudit>();
        public DbSet<PageMetaTag> PageMetaTags => Set<PageMetaTag>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<GeoIpRange> GeoIpRanges => Set<GeoIpRange>();
        public DbSet<SiteRequest> SiteRequests => Set<SiteRequest>();
        public DbSet<PostStatistic> PostStatistics => Set<PostStatistic>();
        //public DbSet<r TemplateMedia> TemplateMedias => Set<TemplateMedia>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new ContactConfig());
            builder.ApplyConfiguration(new EmailSubscriberConfig());
            builder.ApplyConfiguration(new LanguageConfig());
            builder.ApplyConfiguration(new TagConfig());
            builder.ApplyConfiguration(new TemplateCategoryConfig());
            builder.ApplyConfiguration(new TemplateConfig());
            builder.ApplyConfiguration(new TemplateTagConfig());
            builder.ApplyConfiguration(new UserAuditConfig());
            builder.ApplyConfiguration(new PageMetaTagConfig());
            builder.ApplyConfiguration(new SiteRequestConfig());
            builder.ApplyConfiguration(new CountryConfig());
            builder.ApplyConfiguration(new PostStatesticConfig());
            //builder.ApplyConfiguration(new TemplateMediaConfig());
        }
    }
}
