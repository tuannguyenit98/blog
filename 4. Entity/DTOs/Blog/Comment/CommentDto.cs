using System;
using System.Collections.Generic;

namespace DTOs.Blog.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int FK_PostId { get; set; }
        public string UserName { get; set; }
        public List<CommentDto> Comments { get; set; }
        public string Content { get; set; }
    }
}
