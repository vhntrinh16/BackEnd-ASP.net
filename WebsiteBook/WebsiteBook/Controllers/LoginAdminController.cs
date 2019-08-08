using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class LoginAdminController : Controller
    {
        BookManagementEntities db = new BookManagementEntities();
        // GET: LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string sUsername = f["txtTaiKhoan"].ToString();
            string sPassword = f.Get("txtMatKhau").ToString();
            User user = db.Users.FirstOrDefault(n => n.Username == sUsername && n.Password == sPassword);
            if (user != null)
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