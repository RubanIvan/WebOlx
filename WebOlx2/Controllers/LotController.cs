using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOlx2.Proc;

namespace WebOlx2.Controllers
{
    public class LotController : Controller
    {
        //вывести объявление
        public ActionResult Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");
            OlxEntities entities = new OlxEntities();
            var lot = (from x in entities.Lot
                       where x.LotID == id
                       select x);
            if (!lot.Any()){ return RedirectToAction("Index", "Home"); }
            Lot model = lot.First();

            ViewBag.LotPath = LotManage.GetCatPath(model.CatId);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        //GET добавить лот
        public ActionResult Create()
        {
            OlxEntities entities = new OlxEntities();
            List<Catalog> model = entities.Catalog.ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        //POST добавить лот
        public ActionResult CreateLot()
        {
            string name = User.Identity.Name;
            string desc = Request.Form["Desc"];
            string title = Request.Form["Title"];
            int catid = int.Parse(Request.Form["CatId"]);
            int price = int.Parse(Request.Form["Price"]);

            OlxEntities entities = new OlxEntities();
            Lot lot = new Lot();
            lot.CatId = catid;
            lot.Date = DateTime.Today;
            lot.Desc = desc;
            lot.Title = title;
            lot.Price = price;
            lot.UserName = name;


            entities.Lot.Add(lot);
            entities.SaveChanges();

            //получаем номер вставленого лота
            int Lotid = (from enty in entities.Lot
                         where enty.Desc == desc &&
                         enty.Title == title &&
                         enty.CatId == catid &&
                         enty.Price == price &&
                         enty.Date == DateTime.Today &&
                         enty.UserName == name
                         select enty.LotID).First();

            //coхраняем картинку под именем лота
            HttpPostedFileBase file = Request.Files["Image"];

            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/UploadImg/";
            file.SaveAs(Path.Combine(path, "Lot_" + Lotid + ".jpg"));

            return RedirectToAction("Index", "Lot",new {id= Lotid });
        }


        [HttpGet]
        [Authorize]
        public ActionResult Dlete()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        //выдать все лоты текущего пользователя
        public JsonResult GetAllLot()
        {
            OlxEntities entities = new OlxEntities();
            var lot = (from x in entities.Lot
                       where x.UserName == User.Identity.Name
                       select x);

            return Json(lot);

        }

        [HttpPost]
        [Authorize]
        public void DelLot(int lotId)
        {
            OlxEntities entities = new OlxEntities();
            var lot = (from x in entities.Lot
                       where x.UserName == User.Identity.Name && x.LotID==lotId
                       select x);
            if (lot.Any())
            {
                entities.Lot.Remove(lot.First());
                entities.SaveChanges();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Content/UploadImg/";
                System.IO.File.Delete(Path.Combine(path, "Lot_" + lotId + ".jpg"));

                
            }
            return;

        }


       
    }
}