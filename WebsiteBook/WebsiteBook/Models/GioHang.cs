using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBook.Models
{
    public class GioHang
    {
        BookManagementEntities db = new BookManagementEntities();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(int MaSach)
        {
            iMaSP = MaSach;
            Book book = db.Books.Single(n => n.BookID == iMaSP);
            sTenSP = book.BookName;
            sHinhAnh = book.CoverImage;
            dDonGia = double.Parse(book.Price.ToString());
            iSoLuong = 1;
        }
    }
}