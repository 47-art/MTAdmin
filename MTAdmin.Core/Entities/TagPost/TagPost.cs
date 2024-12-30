namespace MTAdmin.Core.Entities.TagPost
{
    public class TagPost
    {
        public int PostId { get; set; }
        public Post.Post Post { get; set; }
        public int TagId { get; set; }
        public PostTag.PostTag PostTag { get; set; }
    }
}
