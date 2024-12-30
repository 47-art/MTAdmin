using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.Post
{
    public class Post : BaseEntity<int>
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string PostMainImg { get; set; }
        public string PostMainImgAlt { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }        
        public string MetaKeywords { get; set; }
        public string MetaDesc { get; set; }
        public ICollection<CategoryPost.CategoryPost> CategoryPosts { get; set; }
        public ICollection<TagPost.TagPost> TagPosts { get; set; }
    }
}
