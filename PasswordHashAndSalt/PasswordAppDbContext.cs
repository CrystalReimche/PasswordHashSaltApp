using Microsoft.EntityFrameworkCore;

namespace PasswordHashAndSalt
{
    public class PasswordAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Connection String
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PasswordHashSalt;Integrated Security=True;");
        }

        // Mapping User class to Users table in database
        public DbSet<User> Users { get; set; }
    }
}
