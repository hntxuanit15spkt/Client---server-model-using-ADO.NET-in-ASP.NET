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
  public class GioHangController : Controller
  {
    // GET: QuanLyGioHang
    Config cf = new Config(Connect.ConnectString);
    public GIOHANG LayGioHang(int MaKH)
    {
      GIOHANG lstGioHang = Session["GioHang"] as GIOHANG;
      if (lstGioHang == null)
      {
        DataTable dt = cf.ExecuteQuery("select * from GIOHANG where MaKH=" + MaKH + " and DaDat=0");
        lstGioHang = new GIOHANG(dt.Rows[0]);
        Session["GioHang"] = lstGioHang;
      }
      return lstGioHang;
    }
    public decimal TinhTongTien()
    {
      //Lấy giỏ hàng
      GIOHANG lstGioHang = Session["GioHang"] as GIOHANG;
      if (lstGioHang == null)
      {
        return 0;
      }
      DataTable dt = cf.ExecuteQuery("select * from GIOHANG where MaGioHang=" + lstGioHang.MaGioHang);
      GIOHANG gh = new GIOHANG(dt.Rows[0]);
      return gh.ThanhTien;
    }
    public double TinhTongSoLuong()
    {
      //Lấy giỏ hàng
      GIOHANG lstGioHang = Session["GioHang"] as GIOHANG;
      if (lstGioHang == null)
      {
        return 0;
      }
      DataTable dt = cf.ExecuteQuery("select * from CHITIETGIOHANG where MaGioHang=" + lstGioHang.MaGioHang);
      List<CHITIETGIOHANG> lstCTGH = new List<CHITIETGIOHANG>();
      CHITIETGIOHANG ctgh = null;
      foreach (DataRow dr in dt.Rows)
      {
        ctgh = new CHITIETGIOHANG(dr);
        lstCTGH.Add(ctgh);
      }
      return lstCTGH.Sum(n => n.SoLuong);
    }
    public ActionResult Index(int MaKH)
    {
      Config cf = new Config(Connect.ConnectString);
      GIOHANG lstGioHang = LayGioHang(MaKH);
      DataTable lstSPTrongGioHang = cf.ExecuteQuery("select * from CHITIETGIOHANG where MaGioHang=" + lstGioHang.MaGioHang);
      List<CHITIETGIOHANG> lstCTGH = new List<CHITIETGIOHANG>();
      CHITIETGIOHANG ctgh = null;
      foreach (DataRow dr in lstSPTrongGioHang.Rows)
      {
        ctgh = new CHITIETGIOHANG(dr);
        lstCTGH.Add(ctgh);
      }
      ViewBag.TongTienDonHang = TinhTongTien();
      return View(lstCTGH);
    }
    public ActionResult GioHangPartial(int? id)
    {
      Config cf = new Config(Connect.ConnectString);
      if (cf.Connection())
      {
        if (TinhTongSoLuong() == 0)
        {
          ViewBag.TongSoLuong = 0;
          ViewBag.TongTien = 0;
          //return PartialView(nd);
        }
        ViewBag.TongSoLuong = TinhTongSoLuong();
        ViewBag.TongTien = TinhTongTien();
        DataTable dt = cf.ExecuteQuery("select * from NGUOIDUNG where MaNguoiDung=" + id);
        NGUOIDUNG nd = new NGUOIDUNG(dt.Rows[0]);
        return PartialView(nd);
      }
      TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
      return RedirectToAction("Index", "Home");
    }

    public ActionResult ThemGioHang(int MaSP, string strURL, int MaKH)
    {
      Config cf = new Config(Connect.ConnectString);
      if (cf.Connection())
      {
        DataTable dtSP = cf.ExecuteQuery("select * from SANPHAM where MaSP=" + MaSP);
        if (dtSP.Rows.Count == 0)
        {
          Response.StatusCode = 404;
          return null;
        }
        SANPHAM sp = new SANPHAM(dtSP.Rows[0]);//Sản phẩm được khách hàng chọn mua
        GIOHANG lstGioHang = LayGioHang(MaKH);
        DataTable dtChiTietGioHang = new DataTable();
        dtChiTietGioHang = cf.ExecuteQuery("select * from CHITIETGIOHANG where MaGioHang=" + lstGioHang.MaGioHang + " and MaSP=" + MaSP);
        if (dtChiTietGioHang.Rows.Count != 0)
        {
          CHITIETGIOHANG spCheck = new CHITIETGIOHANG(dtChiTietGioHang.Rows[0]);
          //nếu một sản phẩm vừa chọn đã có trong bảng CHITIETGIOHANG của GIOHANG lstGioHang
          if (sp.SoLuongTon < spCheck.SoLuong)
          {
            return View("ThongBao");
          }
          spCheck.SoLuong++;
          cf.ExecuteNonQuery("update CHITIETGIOHANG set SoLuong=" + spCheck.SoLuong + " from CHITIETGIOHANG where MaGioHang=" + lstGioHang.MaGioHang + " and MaSP=" + MaSP);
          //DataTable dtGioHang = cf.ExecuteQuery("select * from GIOHANG where MaGioHang=" + spCheck.MaGioHang);
          //GIOHANG gioHang = new GIOHANG(dtGioHang.Rows[0]);
          //gioHang.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
          //gioHang.ThanhTien = spCheck.SoLuong * sp.DonGia;
          return Redirect(strURL);
        }
        //dtChiTietGioHang = cf.ExecuteQuery("select * from CHITIETGIOHANG where MaSP=" + MaSP);
        //if (dtChiTietGioHang.Rows.Count != 0)
        //{
        //  CHITIETGIOHANG itemGH = new CHITIETGIOHANG(dtChiTietGioHang.Rows[0]);
        //  if (sp.SoLuongTon < itemGH.SoLuong)
        //  {
        //    return View("ThongBao");
        //  }
        //  cf.ExecuteNonQuery("Insert into CHITIETGIOHANG values (" + lstGioHang.MaGioHang + ", " + itemGH.MaSP + "," + itemGH.SoLuong + ")");
        //}
        //else
        //{
        cf.ExecuteNonQuery("Insert into CHITIETGIOHANG values (" + lstGioHang.MaGioHang + ", " + sp.MaSP + ",1)");
        return Redirect(strURL);
        //}
      }
      TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
      return RedirectToAction("Index", "Home");
    }
    public ActionResult CapNhatGioHang(int maGioHang, int maSP, int soLuong, string quantity)
    {
      //Kiểm tra số lượng tồn 
      DataTable data = cf.ExecuteQuery("select * from SANPHAM where MaSP=" + maSP);
      SANPHAM spCheck = new SANPHAM(data.Rows[0]);
      if (spCheck.SoLuongTon < soLuong)
      {
        return View("ThongBao");
      }
      DataTable dt = cf.ExecuteQuery("select * from GIOHANG where MaGioHang=" + maGioHang);
      GIOHANG gh = LayGioHang(new GIOHANG(dt.Rows[0]).MaKH);
      if(quantity!=)
      cf.ExecuteNonQuery("update CHITIETGIOHANG set SoLuong = " + quantity + " where MaGioHang=" + maGioHang + " and MaSP=" + maSP);
      return RedirectToAction("Index", new { @MaKH=gh.MaKH });
    }
  }
}