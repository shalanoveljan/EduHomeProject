namespace EduHome.Core.DTOs.Comment
{
    public class CommentPostDto
    {
        public string Text { get; set; }
        public int UserId { get; set; }

        public int BlogId { get; set; }
    }
}
