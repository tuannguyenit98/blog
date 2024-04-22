using Microsoft.AspNetCore.Http;

namespace DTOs.Blog.Post
{
    public class UpdatePostDto
    {
        public int FK_CategoryId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public IFormFile File { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
