using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.BlogConfigurations
{
    public class CategoryConfiguration : FullEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.ToTable("categories");

            builder.Property(e => e.Title).HasColumnName("title");

            builder.Property(e => e.MetaTitle).HasColumnName("meta_title");

            builder.Property(e => e.ParentId).IsRequired(false).HasColumnName("parent_id");

            builder.Property(e => e.Slug).HasColumnName("slug");
        }
    }
}
