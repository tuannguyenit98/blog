using Common.Helpers;
using Entities.Blog;
using System.Linq;
using System;

namespace EntityFrameworkCore.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    UserName = "Admin",
                    Password = EncryptPassword("123456"),
                    Role = "Admin",
                    CreatedAt = DateTime.Now,
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                });
            }

            context.SaveChanges();
        }

        public static string EncryptPassword(string passsword)
        {
            return LoginHelper.EncryptPassword(passsword);
        }
    }
}
