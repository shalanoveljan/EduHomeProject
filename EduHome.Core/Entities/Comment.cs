using EduHome.Core.Entities.BaseEntities;

namespace EduHome.Core.Entities
{
    /// <summary>
    /// Bu Blog Ucun Comment classidir
    /// </summary>
    public class Comment:BaseEntity
    {
        public string Text { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int AspNetUsersId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
