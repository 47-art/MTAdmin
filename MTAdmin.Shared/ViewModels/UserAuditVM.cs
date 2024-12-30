using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using System;

namespace MTAdmin.Shared.ViewModels
{
    public class UserAuditVM
    {
        public string UserName { get; set; }
        public string Event { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string IPAddress { get; set; }
    }
    public class UserAuditParameters : QueryStringParameters
    {
        public AuditEventType EventId { get; set; }
    }
}
