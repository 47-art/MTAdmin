using System.ComponentModel;

namespace MTAdmin.Shared.Enums
{
    public enum AuditEventType
    {
        Login =1,
        [Description("Failed Login")]
        FailedLogin=2,
        [Description("Log Out")]
        LogOut=3
    }
    public enum PostTypeEnum
    {
        Template = 1,
        Category = 2
    }
    public enum PostEventTypeEnum
    {
        View=1,
        Download=2,
        CopyLinkShare=3,
        FBShare=4,
        RedditShare=5,
        InstaShare=6,
        TwitterShare=7,
        TelegramShare=8
    }
}
