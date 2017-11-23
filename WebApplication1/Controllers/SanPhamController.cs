using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Helper;
using System.Data;

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
          public ActionResult DanhSachSPKHDaMua()
          {

               return View();
          }
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
                    return RedirectToAction("Index", "Home");
               }
               return RedirectToAction("Show", "Home");
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
                    return RedirectToAction("Index", "Home");
               }
               return RedirectToAction("Show", "Home");
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
          public ActionResult DanhSachSanPhamTheoLoai(int MaLoaiSP/*, int? page*/)
          {
               Config cf = new Config(Connect.ConnectString);
               if (Connect.CheckConnection())
               {
                    DataTable dt = cf.ExecuteQuery("select * from func_DanhSachSanPhamTheoLoai(" + MaLoaiSP + ")");
                    List<SANPHAM> listSP = new List<SANPHAM>();
                    SANPHAM sp = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                         sp = new SANPHAM(dr);
                         listSP.Add(sp);
                    }
                    //int PageSize = 1;//số sản phẩm trên trang
                    //int PageNumber = (page ?? 1);//page không có giá trị thì PageNumber sẽ có giá trị là 1, số trang hiện tại
                    ViewBag.MaLoaiSP = MaLoaiSP;
                    //return View(listSP.ToPagedList(PageNumber, PageSize));
                    return View(listSP);
               }
               else
               {
                    TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                    return RedirectToAction("Index", "Home");
               }
          }
     }
}