using DTOs.Blog.Comment;
using System;
using System.Collections.Generic;

namespace DTOs.Blog.Post
{
    public class PostDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public string Content { get; set; }
        public long TotalComment { get; set; }
        public DateTime? DeleteAt { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
