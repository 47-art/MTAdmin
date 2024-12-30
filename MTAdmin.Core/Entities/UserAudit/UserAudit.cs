using MTAdmin.Shared.Enums;

namespace MTAdmin.Core.Entities.UserAudit
{
    public class UserAudit
    {
        public int Id { get; set; }
        public AuditEventType Event { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTime.UtcNow;
        public string IPAddress { get; set; }
    }
}
