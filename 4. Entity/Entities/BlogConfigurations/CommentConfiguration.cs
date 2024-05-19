using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.BlogConfigurations
{
    public class CommentConfiguration : FullEntityConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);
            builder.ToTable("comments");

            builder.Property(e => e.ParentId).IsRequired(false).HasColumnName("parent_id");

            builder.Property(e => e.Content).HasColumnName("content");

            builder.Property(e => e.FK_PostId).HasColumnName("fk_post_id");
            builder.HasOne(e => e.Post).WithMany(e => e.Comments).HasForeignKey(e => e.FK_PostId);

            builder.Property(e => e.UserName).HasColumnName("user_name");
        }
    }
}
