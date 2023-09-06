using Common.Helpers;

namespace EntityFrameworkCore.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.EnsureCreated();

            
            context.SaveChanges();
        }

        public static string EncryptPassword(string passsword)
        {
            return LoginHelper.EncryptPassword(passsword);
        }
    }
}
