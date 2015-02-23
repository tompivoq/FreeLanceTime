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

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Project>().Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Project>().HasKey(d => d.Id);
        }
    }
}