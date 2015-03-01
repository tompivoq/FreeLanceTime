using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FreeLance.Model
{
    public class MyContext : DbContext, IMyContext
    {
        public MyContext() { }
        public MyContext(string connectionString) : base(connectionString) { }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<TimeEntry> TimeEntries { get; set; }
        public IDbSet<Client> Clients { get; set; } 

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Project>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Project>().HasKey(p => p.Id);

            builder.Entity<TimeEntry>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<TimeEntry>().HasKey(t => t.Id);

            builder.Entity<Client>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Client>().HasKey(c => c.Id);
        }
    }
}