namespace DTOs.Blog.Post
{
    public class UpdatePostDto
    {
        public int FK_CategoryId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
