namespace DTOs.Blog.Comment
{
    public class CreateOrUpdateCommentDto
    {
        public int FK_PostId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
    }
}
