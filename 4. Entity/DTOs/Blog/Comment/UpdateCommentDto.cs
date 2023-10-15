namespace DTOs.Blog.Comment
{
    public class UpdateCommentDto
    {
        public int FK_PostId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
    }
}
