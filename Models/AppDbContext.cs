using System.Data.Entity;

namespace LoginRegistrationMVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
