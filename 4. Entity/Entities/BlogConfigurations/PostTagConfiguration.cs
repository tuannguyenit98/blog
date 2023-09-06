using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.BlogConfigurations
{
    public class PostTagConfiguration : FullEntityConfiguration<PostTag>
    {
        public override void Configure(EntityTypeBuilder<PostTag> builder)
        {
            base.Configure(builder);
            builder.ToTable("post_tags");

            builder.Property(e => e.FK_PostId).HasColumnName("fk_post_id");
            builder.HasOne(e => e.Post).WithMany(e => e.PostTags).HasForeignKey(e => e.FK_PostId);

            builder.Property(e => e.FK_TagId).HasColumnName("fk_tag_id");
            builder.HasOne(e => e.Tag).WithMany(e => e.PostTags).HasForeignKey(e => e.FK_TagId);
        }
    }
}
