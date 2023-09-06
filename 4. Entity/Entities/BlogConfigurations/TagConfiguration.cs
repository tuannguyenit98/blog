using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.BlogConfigurations
{
    public class TagConfiguration : FullEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);
            builder.ToTable("tags");

            builder.Property(e => e.Title).HasColumnName("title");

            builder.Property(e => e.MetaTitle).HasColumnName("meta_title");

            builder.Property(e => e.Slug).HasColumnName("slug");
        }
    }
}
