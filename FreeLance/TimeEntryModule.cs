using System;
using System.Data.Entity;
using FreeLance.Model;
using Nancy;
using Nancy.ModelBinding;

namespace FreeLance
{
    /**
     * This module is responsible for recording what time is spent on which project.
     */
    public class TimeEntryModule : NancyModule
    {
        public TimeEntryModule(IMyContext ctx) : base("/recordTime")
        {
            Post["/{projectid}"] = _ =>
            {
                TimeEntry newEntry = this.Bind();
                int projectId = _.projectid; // Need to cast the parameter to int so LINQ knows its working with the correct type
                newEntry.Project = ctx.Projects.Find(projectId);
                newEntry.RegistrationDate = DateTime.Now;
                ctx.TimeEntries.Add(newEntry);
                ctx.SaveChanges();

                return Response.AsJson<object>(newEntry);
            };

            Post["/del/{id}"] = x =>
            {
                var entry = new TimeEntry() { Id = x.id };
                ctx.Entry(entry).State = EntityState.Deleted;

                ctx.SaveChanges();

                return Response.AsText("Time entry successfully deleted");
            };
        }
    }

}