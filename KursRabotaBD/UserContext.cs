using System.Data.Entity;

namespace KursRabotaBD
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("baseConnection")
        { }

        public DbSet<User> Users { get; set; }
    }
}
