using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class NguoiDungController : Controller
    {
        BookManagementEntities db = new BookManagementEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangKy(User user)
        {
            if (ModelState.IsValid)
            {
                //Chèn dữ liệu
                db.Users.Add(user);
                //Lưu vào csdl
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sUsername = f["txtTaiKhoan"].ToString();
            string sPassword = f.Get("txtMatKhau").ToString();
            User user = db.Users.FirstOrDefault(n => n.Username == sUsername && n.Password == sPassword);
            if(user != null)
            {
                ViewBag.ThongBao = "Login Successfully!";
                Session["Username"] = user;
                return View();
            }
            ViewBag.ThongBao = "Username or Password is wrong!";
            return View();
        }
    }
}