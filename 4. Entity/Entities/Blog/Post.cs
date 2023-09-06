using Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Entities.Blog
{
    public class Post : IFullEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int FK_UserId { get; set; }
        public int FK_CategoryId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
