using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebOlx2.Models
{
    public class MainPageModel
    {
        /// <summary>Каталог верхнего уровня</summary>
        public List<Catalog> RootCatalog;

        /// <summary>Последние лоты</summary>
        public List<Lot> Lots;
    }
}