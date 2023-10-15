using Microsoft.AspNetCore.Http;
using System;

namespace DTOs.Blog.Post
{
    public class CreatePostDto
    {
        public int FK_CategoryId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public IFormFile File { get; set; }
        public string Content { get; set; }
    }
}
