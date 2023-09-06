using Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.BlogConfigurations
{
    public class UserConfiguration : FullEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable("users");

            builder.Property(e => e.UserName).HasColumnName("user_name");
            builder.Property(e => e.FullName).HasColumnName("full_name");

            builder.Property(e => e.Email).HasColumnName("email");

            builder.Property(e => e.PassWord).HasColumnName("password");

            builder.Property(e => e.Role).HasColumnName("role");

            builder.Property(e => e.RefreshToken).HasColumnName("refresh_token");

        }
    }
}
