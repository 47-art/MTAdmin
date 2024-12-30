using MTAdmin.Shared.Enums;

namespace MTAdmin.Core.Entities.PostStatestic
{
    public class PostStatistic
    {
        public long Id { get; set; }
        public int? PostId { get; set; }
        public PostTypeEnum PostType { get; set; }
        public PostEventTypeEnum PostEventType { get; set; }
        public string Identity { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
