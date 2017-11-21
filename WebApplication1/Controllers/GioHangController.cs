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
    public GIOHANG LayGioHang(int MaKH)
    {
      Config cf = new Config(Connect.ConnectString);
      GIOHANG lstGioHang = Session["GioHang"] as GIOHANG;
      if (lstGioHang == null)
      {
        DataTable dt = cf.ExecuteQuery("select * from GIOHANG where MaKH=" + MaKH + " and DaDat=0");
        lstGioHang = new GIOHANG(dt.Rows[0]);
        Session["GioHang"] = lstGioHang;
      }
      return lstGioHang;
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
      return View(lstCTGH);
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
          //DataTable dtGioHang = cf.ExecuteQuery("select * from GIOHANG where MaGioHang=" + spCheck.MaGioHang);
          //GIOHANG gioHang = new GIOHANG(dtGioHang.Rows[0]);
          //gioHang.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
          //gioHang.ThanhTien = spCheck.SoLuong * sp.DonGia;
          return Redirect(strURL);
        }
        dtChiTietGioHang = cf.ExecuteQuery("select * from CHITIETGIOHANG where MaSP=" + MaSP);
        CHITIETGIOHANG itemGH = new CHITIETGIOHANG(dtChiTietGioHang.Rows[0]);
        if (sp.SoLuongTon < itemGH.SoLuong)
        {
          return View("ThongBao");
        }
        cf.ExecuteNonQuery("Insert into CHITIETGIOHANG values (" + lstGioHang.MaGioHang + ", " + itemGH.MaSP + "," + itemGH.SoLuong + ")");
      }
      else
      {

      }
      return Redirect(strURL);
    }
  }
}