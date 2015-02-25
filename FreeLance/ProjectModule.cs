using System.Collections.Generic;
using System.Linq;
using FreeLance.Model;
using Nancy;
using Nancy.ModelBinding;

namespace FreeLance
{
    /**
     * This module is responsible for recording what time is spent on which project.
     */
    public class ProjectModule : NancyModule
    {
        private static readonly string MESSAGE_KEY = "SessionMessage";
        private static readonly string RESP_CODE_KEY = "RespCode";

        public ProjectModule(IMyContext ctx)
            : base("/projects")
        {
            Get["/list"] = parameters =>
            {
                var p = new ProjectModel(ctx.Projects.ToList());
                return View["ListProjects", p];
            };

            Get["/list/all"] = parameters =>
            {
                return Response.AsJson<object>(ctx.Projects.ToArray());
            };

            Get["/create"] = parameters =>
                {
                    var message = Session[MESSAGE_KEY]
                    var p = new ProjectModel() {Message = }
                    return View["CreateProject"]
                };

            Post["/create"] = _ =>
            {
                var project = this.Bind<Project>();

                ctx.Projects.Add(project);
                ctx.SaveChanges();

                Session[MESSAGE_KEY] = "Successfully added project " + project.Title;
                Session[RESP_CODE_KEY] = HttpStatusCode.OK;

                return Response.AsRedirect("/create");
            };

            Put["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Delete["/{id:int}"] = x =>
            {
                return HttpStatusCode.NotImplemented;
            };
        }
    }

}