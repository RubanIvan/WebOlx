using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOlx2.Models;

namespace WebOlx2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MainPageModel model =new MainPageModel();
            OlxEntities enties =new OlxEntities();

            model.RootCatalog = enties.Catalog.Where(c => c.ParentID == 0).OrderBy(c => c.Name).ToList();
            model.Lots = enties.Lot.OrderByDescending(l => l.Date).Take(6).ToList();

            return View(model);
        }

       
    }
}