using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.PostCategory
{
    public class PostCategory : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Desc { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDesc { get; set; }
        public ICollection<CategoryPost.CategoryPost> CategoryPosts { get; set; }
    }
}
