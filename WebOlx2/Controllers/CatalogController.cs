using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOlx2.Proc;

namespace WebOlx2.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index(int? id,int page=1)
        {
            if (id == null) return RedirectToAction("Index", "Lot");

            OlxEntities entities = new OlxEntities();

            List<Lot> lot = new List<Lot>();
            foreach (int item in LotManage.GetAllSubCat((int)id))
            {
                lot.AddRange((from x in entities.Lot
                              where x.CatId == item
                              select x).ToList());
            }
            lot.Where(l => l.CatId > 1);
            int MaxPage = (int)Math.Ceiling((lot.Count / ((float)Constants.LotPerPage)));
            ViewBag.Pangination = PagePangination.GetPangination(page, MaxPage, $@"/Catalog/Index/{id}/?page=","1");
            //lot =lot.GetRange((page-1)* Constants.LotPerPage, Constants.LotPerPage);
            lot =lot.Skip((int)(page - 1) * Constants.LotPerPage).Take(Constants.LotPerPage).ToList();

            return View(lot);
        }
    }
}