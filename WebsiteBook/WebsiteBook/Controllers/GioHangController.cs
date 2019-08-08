using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteBook.Models;

namespace WebsiteBook.Controllers
{
    public class GioHangController : Controller
    {
        BookManagementEntities db = new BookManagementEntities();
        #region Giỏ hàng
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if (lsGioHang == null)
            {
                lsGioHang = new List<GioHang>();
                Session["GioHang"] = lsGioHang;
            }
            return lsGioHang;
        }

        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == iMaSach);
            if(book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            GioHang sanpham = lsGioHang.Find(n => n.iMaSP == iMaSach);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                lsGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == iMaSP);
            if(book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            GioHang sach = lsGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sach != null)
            {
                sach.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int iMaSP)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookID == iMaSP);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            GioHang sach = lsGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sach != null)
            {
                lsGioHang.RemoveAll(n => n.iMaSP == iMaSP);
            }
            if (lsGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lsGioHang = LayGioHang();
            return View(lsGioHang);
        }

        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int TongSoLuong = 0;
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if (lsGioHang != null)
            {
                TongSoLuong = lsGioHang.Sum(n => n.iSoLuong);
            }
            return TongSoLuong;
        }

        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if (lsGioHang != null)
            {
                dTongTien = lsGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }

        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lsGioHang = LayGioHang();
            return View(lsGioHang);
        }
        #endregion
        #region Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập
            if (Session["Username"] == null || Session["Username"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //Kiểm tra giỏ hàng
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            Order order = new Order();
            User user = (User)Session["Username"];
            List<GioHang> gh = LayGioHang();
            order.UserID = user.UserID;
            order.NgayDat = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            //Thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = order.OrderID;
                orderDetail.BookID = item.iMaSP;
                orderDetail.SoLuong = item.iSoLuong;
                orderDetail.DonGia = (decimal)item.dDonGia;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }

        #endregion
    }
}