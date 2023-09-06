using Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entities.BlogConfigurations
{
    public class FullEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IFullEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id").IsRequired(true);
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.CreatedBy).IsRequired(false).HasColumnName("created_by");
            builder.Property(e => e.UpdatedAt).IsRequired(false).HasColumnName("updated_at");
            builder.Property(e => e.UpdatedBy).IsRequired(false).HasColumnName("updated_by");
            builder.Property(e => e.DeleteAt).IsRequired(false).HasColumnName("delete_at");
        }
    }
}
