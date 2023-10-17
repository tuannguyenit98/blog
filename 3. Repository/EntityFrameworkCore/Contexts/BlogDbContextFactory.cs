using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EntityFrameworkCore.Contexts
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //Using this configure to inform that the Datatime type will be generated to "timestamp without time zone" instead of "timestamp with time zone"
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("connectionconfig.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("BlogConnectionString"));

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}
