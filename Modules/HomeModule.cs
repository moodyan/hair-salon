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
    }
  }
}
