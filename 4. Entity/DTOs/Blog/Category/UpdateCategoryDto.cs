using System;

namespace DTOs.Blog.Category
{
    public class UpdateCategoryDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
    }
}
