using System;

namespace DTOs.Blog.Post
{
    public class CreatePostDto
    {
        public int FK_UserId { get; set; }
        public int FK_CategoryId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
