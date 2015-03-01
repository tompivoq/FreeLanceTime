using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using FreeLance.Model;
using Nancy;
using Nancy.ModelBinding;

namespace FreeLance
{
    /**
     * This module is responsible for recording what time is spent on which project.
     */
    public class ClientModule : NancyModule
    {
        public ClientModule(IMyContext ctx) : base("/clients")
        {
            Get["/list"] = parameters =>
            {
                var model = new ResponseBean() {Clients = ctx.Clients.ToList()};
                return View["ListClients", model];
            };

            Get["/create"] = parameters =>
            {
                return View["CreateClient"];
            };

            Post["/create"] = _ =>
            {
                var message = "Successfully added client ";
                var createSuccess = "alert-success";
                try
                {
                    var client = this.Bind<Client>();
                    client.CreationDate = DateTime.Now;

                    ctx.Clients.Add(client);
                    ctx.SaveChanges();
                    message += " " + client.CompanyName;
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
                var model = new ResponseBean() { Message = message, CreateSuccess = createSuccess, Redirect = true};
                return View["CreateClient", model];
            };

            Get["/{id}"] = _ =>
            {
                ResponseBean model = new ResponseBean();
                Client c;
                int clientId = _.id;
                if ((c = ctx.Clients.Find(clientId)) != null)
                {
                    model.Client = c;
                    var projects = ctx.Projects.Where(p => p.Client.Id == clientId).ToList();
                    model.Projects = projects;
                    model.Success = true; // We need to send a bool, since the SSVE @If can only check on bools
                }
                else
                {
                    model.Message = "Couldn't find client with id " + _.id;
                    model.Success = false;
                }
                return View["ClientDetail", model];
            };
        }
    }
}