using FreeLance.Model;
using Nancy;

namespace FreeLance
{
    /**
     * This module is responsible for recording what time is spent on which project.
     */
    public class ProjectModule : NancyModule
    {
        public ProjectModule(IMyContext ctx)
            : base("/projects")
        {
            Get["/create"] = parameters => View["CreateProject"];
            Get["/list"] = parameters => View["ListProjects"];
        }
    }

}