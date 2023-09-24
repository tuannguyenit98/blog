using System;

namespace DTOs.Blog.Category
{
    public class CreateCategoryDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
    }
}
