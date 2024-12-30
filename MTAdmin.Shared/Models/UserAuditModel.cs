using MTAdmin.Shared.Enums;

namespace MTAdmin.Shared.Models
{
    public class UserAuditModel
    {
        public AuditEventType Event { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
    }
    public class UserIdAndNameModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
    public class MetaDataModel
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
