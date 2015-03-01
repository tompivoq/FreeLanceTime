using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using FreeLance.Model;
using Nancy.IO;

namespace FreeLance
{
    public class HtmlUtil
    {
        /**
        * Formats Exceptions in HTML so they are pretty when being displayed on the page.
        * Recursively styles exception-messages and inner exceptions
        */
        public static String FormatExceptionHtml(Exception ex)
        {
            StringBuilder res = new StringBuilder();
            res.Append("<i>");
            res.Append(ex.GetType());
            res.Append("</i><br>");
            res.Append("<p class=error-message>");
            res.Append(ex.Message);
            if (ex.InnerException != null)
            {
                res.Append("<strong>Inner Exception</strong>");
                res.Append(FormatExceptionHtml(ex.InnerException));
            }
            res.Append("</p>");
            return res.ToString();
        }
    }

    public static class RequestBodyExtensions
    {
        public static string ReadAsString(this RequestStream requestStream)
        {
            using (var reader = new StreamReader(requestStream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /*
     * Container class used for sending data to the ViewEngine.
     * Should be more split; currently it just contains properties for each possible View, 
     * and is used differently in 3 different places (see ProjectModule)
     */
    public class ResponseBean
    {
        // Properties used in ListProjects
        public List<Project> Projects { get; set; }
        // properties for DetailsViews
        public Project Project { get; set; }
        public Client Client { get; set; }
        // Dictionary for ClientDetails, so that we get Projects coupled with the time spent (computed from TimeEntries)
        public Dictionary<Project, float> ProjectDict { get; set; }
        public List<TimeEntry> WorkDone { get; set; }
        private float _HoursSpent;

        public float HoursSpent
        {
            get { return _HoursSpent; }
            set
            {
                _HoursSpent = value;
                if (Project != null)
                {
                    HoursLeft = Project.HoursAllocated - value;
                }
                else
                {
                    HoursLeft = float.NaN;
                }
            }
        }

        public float HoursLeft { get; set; }
        public bool Success { get; set; }
        // Properties used for Project/Client Creation
        public String Message { get; set; }
        public String CreateSuccess { get; set; }
        public bool Redirect { get; set; }
        public List<Client> Clients { get; set; }

        public ResponseBean() { }
        public ResponseBean(List<Project> p)
        {
            this.Projects = p;
        }
    }
}