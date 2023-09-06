using Entities.Interfaces;
using System;

namespace Entities.Blog
{
    public class PostTag : IFullEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int FK_PostId { get; set; }
        public int FK_TagId { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
