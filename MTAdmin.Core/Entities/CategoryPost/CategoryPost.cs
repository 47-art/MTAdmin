namespace MTAdmin.Core.Entities.CategoryPost
{
    public class CategoryPost
    {
        public int PostId { get; set; }
        public Post.Post Post { get; set; }
        public int PostCategoryId { get; set; }
        public PostCategory.PostCategory PostCategory { get; set; }
    }
}
