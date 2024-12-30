using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.PostTag
{
    public class PostTag : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Desc { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDesc { get; set; }
        public ICollection<TagPost.TagPost> TagPosts { get; set; }
    }
}
