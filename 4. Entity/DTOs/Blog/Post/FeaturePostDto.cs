using DTOs.Blog.Post;
using System.Collections.Generic;

namespace DTOs.Blog
{
    public class FeaturePostDto
    {
        public PostDto PostFeature { get; set; }
        public List<PostDto> PostViews { get; set; }
    }
}
