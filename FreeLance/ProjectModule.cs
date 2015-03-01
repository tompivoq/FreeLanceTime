using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
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
        public ProjectModule(IMyContext ctx)
            : base("/projects")
        {
            Get["/list"] = parameters =>
                {
                    var model = new ResponseBean(ctx.Projects.ToList());
                    return View["ListProjects", model];
                };

            Get["/create"] = parameters =>
                {
                    var clients = new ResponseBean() {Clients = ctx.Clients.ToList()};
                    return View["CreateProject", clients];
                };

            Post["/create"] = _ =>
                {
                    var message = "Successfully added project ";
                    var createSuccess = "alert-success";
                    try
                    {
                        string reqString = this.Request.Body.ReadAsString();
                        int clientId = getClientIdFromRequest(reqString);
                        var client = ctx.Clients.Find(clientId);
                        var project = this.Bind<Project>("Client");
                        project.CreationDate = DateTime.Now;
                        project.Client = client;

                        ctx.Projects.Add(project);
                        ctx.SaveChanges();
                        message += project.Title;
                    }
                    catch (Exception ex)
                    {
                        if (ex is ModelBindingException)
                        {
                            message = "Couldn't bind model. <br>" +
                                      HtmlUtil.FormatExceptionHtml(ex);
                            createSuccess = "alert-warning";
                        }
                        else if (ex is DbUpdateException || ex is RequestExecutionException)
                        {
                            message = "Couldn't complete request to DataBase. <br>" +
                                      HtmlUtil.FormatExceptionHtml(ex);
                            createSuccess = "alert-danger";
                        }
                        else
                        {
                            message = "Something went wrong while creating your project :/<br>" +
                                      HtmlUtil.FormatExceptionHtml(ex);

                            createSuccess = "alert-danger";
                        }
                    }
                    var model = new ResponseBean()
                        {
                            Message = message,
                            CreateSuccess = createSuccess,
                            Redirect = true,
                            Clients = ctx.Clients.ToList()
                        };
                    return View["CreateProject", model];
                };

            Get["/{id}"] = _ =>
                {
                    ResponseBean model = new ResponseBean();
                    Project p;
                    int projectId = _.id;
                    if ((p = ctx.Projects.Find(projectId)) != null)
                    {
                        model.Project = p;
                        model.Client = p.Client;
                        var timeEntries = ctx.TimeEntries.Where(t => t.Project.Id == projectId).ToList();
                        float hoursSpent = timeEntries.Sum(t => t.HoursSpent);
                        model.WorkDone = timeEntries;
                        model.HoursSpent = hoursSpent;
                        model.Success = true; // We need to send a bool, since the SSVE @If can only check on bools
                    }
                    else
                    {
                        model.Message = "Couldn't find project with id " + _.id;
                        model.Success = false;
                    }
                    return View["ProjectDetail", model];
                };

            Post["/del/{id}"] = x =>
            {
                var project = new Project() { Id = x.id };
                ctx.Entry(project).State = EntityState.Deleted;

                ctx.SaveChanges();

                return HttpStatusCode.OK;
            };
        }

        private int getClientIdFromRequest(String request)
        {
            String searchString = "Client=";
            int startIndex = request.IndexOf(searchString);
            startIndex = startIndex + searchString.Length;
            int length = request.IndexOf('&', startIndex) - startIndex;
            String id = request.Substring(startIndex, length);
            return int.Parse(id);
        }
    }
}