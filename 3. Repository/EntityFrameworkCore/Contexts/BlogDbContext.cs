using Entities.Blog;
using Entities.BlogConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace EntityFrameworkCore.Contexts
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //Using this configure to inform that the Datatime type will be generated to "timestamp without time zone" instead of "timestamp with time zone"
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("connectionconfig.json", false, true)
                .Build();
            var connectionString = configuration.GetConnectionString("BlogConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
