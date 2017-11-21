using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
  public class HomeController : Controller
  {

    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Show(string ip, string Databasename, string usr, string pwd)
    {
      Connect.ipaddress = ip;
      Connect.databasename = Databasename;
      Connect.username = usr;
      Connect.password = pwd;
      Connect.ConnectString = " Data Source=" + ip + ";Initial Catalog=" + Databasename + ";Integrated Security=False;User ID=" + usr + ";Password=" + pwd;
      Config cf = new Config(Connect.ConnectString);
      if (Connect.CheckConnection())
      {
        DataTable dtNguoiDung = cf.ExecuteQuery("select * from NGUOIDUNG where TaiKhoan ='" + Connect.username + "'and MatKhau='" + Connect.password + "'");
        NGUOIDUNG nguoiDung = new NGUOIDUNG(dtNguoiDung.Rows[0]);
        TempData["result"] = "Kết nối thành công!";
        if (nguoiDung.MaLoaiNguoiDung != 1 && nguoiDung.MaLoaiNguoiDung != 2)
        {
          return RedirectToAction("Index", "QuanLySanPham");
        }
      }
      else
      {
        TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
        return RedirectToAction("Index", "Home");
      }
      return View();
    }
    public ActionResult DisConnect()
    {
      Connect.ConnectString = "";
      return RedirectToAction("Index", "Home");
    }
    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }
    public ActionResult DangKy()
    {
      return View();
    }
    public ActionResult HeaderTopPartial()
    {
      Config cf = new Config(Connect.ConnectString);
      //List<NGUOIDUNG> listNguoiDung = new List<NGUOIDUNG>();
      DataTable dtNguoiDung = cf.ExecuteQuery("select * from NGUOIDUNG where TaiKhoan ='" + Connect.username + "'and MatKhau='" + Connect.password + "'");
      NGUOIDUNG nguoiDung = new NGUOIDUNG(dtNguoiDung.Rows[0]);
      return View(nguoiDung);
    }
    public ActionResult MenuPartial()
    {
      Config cf = new Config(Connect.ConnectString);
      DataTable dtNguoiDung = cf.ExecuteQuery("select * from NGUOIDUNG where TaiKhoan ='" + Connect.username + "'and MatKhau='" + Connect.password + "'");
      NGUOIDUNG nguoiDung = new NGUOIDUNG(dtNguoiDung.Rows[0]);
      //var lstSP = db.SanPhams;
      List<LOAISANPHAM> listLSP = new List<LOAISANPHAM>();
      LOAISANPHAM lsp = null;
      DataTable dtSP = cf.ExecuteQuery("select * from LOAISANPHAM");
      ViewBag.MaKH = nguoiDung.MaNguoiDung;
      foreach (DataRow item in dtSP.Rows)
      {
        lsp = new LOAISANPHAM(item);
        listLSP.Add(lsp);
      }
      return PartialView(listLSP);
    }
    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      return View();
    }
  }
}