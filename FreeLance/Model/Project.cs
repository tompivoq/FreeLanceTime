using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace FreeLance.Model
{
    public class Project
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String SubTitle { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float HoursAllocated { get; set; }
    }

    public class ProjectModel
    {
        public List<Project> Projects { get; set; }
        public String Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ProjectModel() {}
        public ProjectModel(List<Project> p )
        {
            this.Projects = p;
        }
    }
}