using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOlx2.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index(int? id)
        {
            return View();
        }
    }
}