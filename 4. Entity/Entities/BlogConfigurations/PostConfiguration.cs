using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.BlogConfigurations
{
    public class PostConfiguration : FullEntityConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);
            builder.ToTable("posts");

            builder.Property(e => e.Title).HasColumnName("title");

            builder.Property(e => e.MetaTitle).HasColumnName("meta_title");

            builder.Property(e => e.Image).HasColumnName("image");

            builder.Property(e => e.Slug).HasColumnName("slug");

            builder.Property(e => e.Status).HasColumnName("status");

            builder.Property(e => e.Content).HasColumnName("content");

            builder.Property(e => e.NumberView).HasColumnName("number_view");

            builder.Property(e => e.FK_CategoryId).HasColumnName("fk_category_id");
            builder.HasOne(e => e.Category).WithMany(e => e.Posts).HasForeignKey(e => e.FK_CategoryId);

            builder.Property(e => e.FK_UserId).HasColumnName("fk_user_id");
            builder.HasOne(e => e.User).WithMany(e => e.Posts).HasForeignKey(e => e.FK_UserId);
        }
    }
}
