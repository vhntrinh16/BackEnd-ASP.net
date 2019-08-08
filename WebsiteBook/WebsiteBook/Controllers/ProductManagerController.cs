using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebsiteBook.Models;
using System.IO;

namespace WebsiteBook.Controllers
{
    public class ProductManagerController : Controller
    {
        BookManagementEntities db = new BookManagementEntities();
        // GET: ProductManager
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.Books.ToList().OrderBy(n => n.BookID).ToPagedList(pageNumber, pageSize));

        }

        //Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Book book, HttpPostedFileBase fileUpload)
        {
            
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            //Kiểm tra đường dẫn ảnh
            if(fileUpload == null)
            {
                ViewBag.ThongBao = "Choose Image";
                return View();
            }
            //Thêm vào csdl
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);

                //Lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/imagebook"), fileName);

                //Kiểm tra hình ảnh 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Image Exists";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                book.CoverImage = fileUpload.FileName;
                db.Books.Add(book);
                db.SaveChanges();
            }
            return View();
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int MaSach)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == MaSach);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(book);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NXBs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return RedirectToAction("Index");
        }

        //Hiển thị
        public ActionResult Details(int MaSach)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == MaSach);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(book);
        }

        //Xóa
        [HttpGet]
        public ActionResult Delete(int MaSach)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == MaSach);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int MaSach)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == MaSach);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}