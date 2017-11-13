using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Test
       
        public SanPhamController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }
        //public List<SanPham> ListAll(string sql)
        //{
        //    var lstSP = 
        //    List < SanPham > = new ListAll()
        //    List<SanPham> listsp = new List<SanPham>();
        //    return listsp;
        //}
        public ActionResult Delete(int id)
        {
            var list = new List<SANPHAM>();
            Config cf = new Config(Connect.ConnectString);
            //var check = cf.Connection();
            if (Connect.CheckConnection())
            {
                TempData["results"] = "Xóa thành công!";
                cf.Delete("Delete SanPham where id = " + id);

            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index", "test");
            }
            return RedirectToAction("Show", "test");
        }
        public ActionResult Update(SANPHAM sp)
        {
            Config cf = new Config(Connect.ConnectString);
            var list = new List<SANPHAM>();
            var check = cf.Connection();
            if (check)
            {
                sp.TenSP = "Bình Trôi";
                TempData["results"] = "Sửa thành công!";
                string sql = "Update  SanPham  set TenSP=N'" + sp.TenSP + "' where MaSP=" + sp.MaSP;
                cf.Update(sql);
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index", "test");
            }
            return RedirectToAction("Show", "test");
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(string TenSP, decimal DonGia, string Mota, string Hinhanh,
          int SoLuongton, int LuotXem, int LuotBinhChon, int MaLoaiSP, bool DaXoa, int MaSukien)
        {
            Config cf = new Config(Connect.ConnectString);
            var list = new List<SANPHAM>();
            var check = cf.Connection();
            if (check)
            {
                TempData["results"] = "Thêm thành công!";
                cf.Insert("INSERT INTO SanPham VALUES(" + TenSP + ", "
                  + DonGia + "," + Mota + "," + Hinhanh + "," + SoLuongton + "," + LuotXem + "," + LuotBinhChon
                  + "," + MaLoaiSP + "," + DaXoa + "," + MaSukien + ");");
                //cf.Insert("INSERT INTO SanPham VALUES(" + TenSP + ");");
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index", "test");
            }
            return RedirectToAction("Show", "test");
        }
        public ActionResult LocSanPhamTheoLoai(string TenLoai)
        {
            //trang show sản phẩm cho khách hàng==> Tìm kiếm
            //
            return View();
        }
    }

}