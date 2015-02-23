using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FreeLance.Model
{
    public interface IMyContext
    {
        IDbSet<Project> Projects { get; set; }
        IDbSet<TimeEntry> TimeEntries { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}