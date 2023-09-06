﻿using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(e => e.FK_UserId).HasColumnName("fk_user_id");
            builder.HasOne(e => e.User).WithMany(e => e.Comments).HasForeignKey(e => e.FK_UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
