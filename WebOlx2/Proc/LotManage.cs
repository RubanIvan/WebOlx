using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOlx2.Proc
{
    public static class LotManage
    {

        /// <summary>получить путь до данного каталога</summary>
        public static List<Catalog> GetCatPath(int CatId)
        {
            List<Catalog> Lots = new List<Catalog>();

            OlxEntities entities = new OlxEntities();
            Catalog c = (from x in entities.Catalog
                         where x.CatalogID == CatId
                         select x).First();
            Lots.Add(c);
            //если это каталог верхнего уровня то выходим
            //if (c.ParentID == 0) return Lots;
            while (c.ParentID != 0)
            {
                int l = c.ParentID;

                c = (from x in entities.Catalog
                     where x.CatalogID == l
                     select x).First();
                Lots.Insert(0, c);
            }
            return Lots;
        }


        /// <summary>получить все подкаталоги данного каталога </summary>
        public static List<int> GetAllSubCat(int CatId)
        {
            OlxEntities entities = new OlxEntities();

            List<int> SubCat = new List<int>();

            SubCat = (from x in entities.Catalog
                      where x.ParentID == CatId
                      select x.CatalogID).ToList();

            List<int> SubSubCat = new List<int>();

            foreach (int s in SubCat)
            {
                SubSubCat.AddRange((from x in entities.Catalog
                                    where x.ParentID == s
                                    select x.CatalogID).ToList());
            }

            SubCat.AddRange(SubSubCat);
            SubCat.Add(CatId);
            return SubCat;
        }
    }
}