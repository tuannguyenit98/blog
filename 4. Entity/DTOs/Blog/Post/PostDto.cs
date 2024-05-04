using System;

namespace DTOs.Blog.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
