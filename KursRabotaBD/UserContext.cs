using System.Data.Entity;

namespace KursRabotaBD
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("KursRabotaBD.Properties.Settings.masterConnectionString")
        { }

        public DbSet<User> Users { get; set; }
    }
}
