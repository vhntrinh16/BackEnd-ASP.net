using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class ProductController : Controller
    {
        BookManagementEntities book = new BookManagementEntities();
        // GET: Product
        public ActionResult ProductPartial()
        {
            return PartialView(book.Books.Take(6).OrderBy(x=>x.BookName).ToList());
        }

       public ViewResult DetailBook(int MaSach = 0)
        {
            Book books = book.Books.SingleOrDefault(n => n.BookID == MaSach);
            if(books == null)
            {
                //Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenChuDe = book.ChuDes.Single(n => n.MaChuDe == books.MaChuDe).TenChuDe;
            ViewBag.TenNXB = book.NXBs.Single(n => n.MaNXB == books.MaNXB).TenNXB;
            return View(books);
        }
    }
}