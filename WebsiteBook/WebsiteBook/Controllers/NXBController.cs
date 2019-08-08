using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class NXBController : Controller
    {
        BookManagementEntities book = new BookManagementEntities();
        // GET: NXB
        public ActionResult NXBPartial()
        {
            return PartialView(book.NXBs.Take(10).OrderBy(x => x.TenNXB).ToList());
        }
    }
}