using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class TheLoaiController : Controller
    {
        BookManagementEntities book = new BookManagementEntities();
        // GET: TheLoai
        public ActionResult TheloaiPartial()
        {
            return PartialView(book.ChuDes.Take(10).ToList());
        }
    }
}