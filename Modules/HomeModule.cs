using Nancy;
using System;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;
using Salon.Objects;

namespace Salon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/stylists"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/stylists/new"] = _ => {
        return View["stylist_add.cshtml"];
      };
      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-specialty"]);
        newStylist.Save();
        return View["success.cshtml"];
      };
      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedStylist = Stylist.Find(parameters.id);
        var stylistClients = selectedStylist.GetClients();
        // var clientFeedback = selectedClient.GetFeedback();
        model.Add("stylist", selectedStylist);
        model.Add("clients", stylistClients);
        // model.Add("feedback", clientFeedback);
        return View["stylist.cshtml", model];
      };
      Get["/clients"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };
      Get["/clients/new"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["client_add.cshtml", allStylists];
      };
      Post["/clients/new"] = _ => {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-details"], Request.Form["stylist-id"]);
        newClient.Save();
        return View["success.cshtml"];
      };
      Get["/clients/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedClient = Client.Find(parameters.id);
        // var clientStylist = selectedStylist.GetClients();
        model.Add("client", selectedClient);
        // model.Add("stylist", clientStylist);
        return View["client.cshtml", model];
      };
    }
  }
}
