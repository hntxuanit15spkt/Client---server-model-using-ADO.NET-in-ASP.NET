using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
  public class QuanLySanPhamController : Controller
  {
    // GET: QuanLySanPham
    Config cf = new Config(Connect.ConnectString);
    public ActionResult Index()
    {
      DataTable dtLstSP = cf.ExecuteQuery("select * from sanpham where daxoa=0");
      List<SANPHAM> lsp = new List<SANPHAM>();
      SANPHAM sp = null;
      foreach (DataRow dr in dtLstSP.Rows)
      {
        sp = new SANPHAM(dr);
        lsp.Add(sp);
      }
      return View(lsp);
    }
    [HttpGet]
    public ActionResult TaoMoi()
    {
      if (cf.Connection())
      {
        //Load dropdownlist Mã Sự kiện và dropdownlist loại sp
        DataTable dtSuKien = cf.ExecuteQuery("select * from SUKIEN");
        DataTable dtLoaiSP = cf.ExecuteQuery("select * from LOAISANPHAM");
        List<LOAISANPHAM> lstLSP = new List<LOAISANPHAM>();
        LOAISANPHAM lsp = null;
        foreach (DataRow item in dtLoaiSP.Rows)
        {
          lsp = new LOAISANPHAM(item);
          lstLSP.Add(lsp);
        }
        List<SUKIEN> lstLSK = new List<SUKIEN>();
        SUKIEN SuKien = null;
        foreach (DataRow item in dtSuKien.Rows)
        {
          SuKien = new SUKIEN(item);
          lstLSK.Add(SuKien);
        }
        ViewBag.MaSuKien = new SelectList(lstLSK, "MaSuKien", "TenSuKien");
        ViewBag.MaLoaiSP = new SelectList(lstLSP, "MaLoaiSP", "TenLoai");
        return View();
      }
      else
      {
        TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
        return RedirectToAction("Index", "Home");
      }
    }
    [ValidateInput(false)]
    [HttpPost]
    public ActionResult TaoMoi(SANPHAM sp, HttpPostedFileBase[] HinhAnh)
    {
      DataTable dtSuKien = cf.ExecuteQuery("select * from SUKIEN");
      DataTable dtLoaiSP = cf.ExecuteQuery("select * from LOAISANPHAM");
      List<LOAISANPHAM> lstLSP = new List<LOAISANPHAM>();
      LOAISANPHAM lsp = null;
      foreach (DataRow item in dtLoaiSP.Rows)
      {
        lsp = new LOAISANPHAM(item);
        lstLSP.Add(lsp);
      }
      List<SUKIEN> lstLSK = new List<SUKIEN>();
      SUKIEN SuKien = null;
      foreach (DataRow item in dtSuKien.Rows)
      {
        SuKien = new SUKIEN(item);
        lstLSK.Add(SuKien);
      }
      ViewBag.MaSuKien = new SelectList(lstLSK, "MaSuKien", "TenSuKien");
      ViewBag.MaLoaiSP = new SelectList(lstLSP, "MaLoaiSP", "TenLoai");
      int loi = 0;
      for (int i = 0; i < HinhAnh.Count(); i++)
      {
        if (HinhAnh[i] != null)
        {
          //Kiểm tra nội dung hình ảnh
          if (HinhAnh[i].ContentLength > 0)
          {
            //Kiểm tra định dạng hình ảnh
            if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/gif" && HinhAnh[i].ContentType != "image/jpg")
            {
              ViewBag.upload += "Hình ảnh" + i + " không hợp lệ <br />";
              loi++;
            }
            else
            {
              //Kiểm tra hình ảnh tồn tại
              //Lấy tên hình ảnh
              var fileName = Path.GetFileName(HinhAnh[0].FileName);
              //Lấy hình ảnh chuyển vào thư mục hình ảnh 
              var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
              //Nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
              if (System.IO.File.Exists(path))
              {
                ViewBag.upload1 = "Hình " + i + "đã tồn tại <br />";
                loi++;
              }
            }
          }
        }
        else
        {
          //db.SANPHAMs.Add(sp);
          cf.ExecuteNonQuery(string.Format("exec stored_ThemSanPham @TenSP = N'{0}', " +
             "@NgayCapNhat = '{1}', @DonGia = {2}, @MoTa = {3}, @HinhAnh = {4}, " +
             "@SoLuongTon = {5}, @LuotXem = {6}, @LuotBinhChon = {7}, @MaLoaiSP = {8}, @DaXoa = 0, " +
             "@NgayDang = '{9}', @MaSuKien = {10} ", sp.TenSP, sp.NgayCapNhat, sp.DonGia, sp.MoTa, sp.HinhAnh,
             sp.SoLuongTon, sp.LuotXem, sp.LuotBinhChon, sp.MaLoaiSP, sp.NgayDang, sp.MaSuKien));
          //db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      if (loi > 0)
      {
        return View(sp);
      }
      sp.HinhAnh = HinhAnh[0].FileName;

      //Kiểm tra hình tổn tại trong csdl chưa
      if (HinhAnh[0].ContentLength > 0)
      {
        //Lấy tên hình ảnh
        var fileName = Path.GetFileName(HinhAnh[0].FileName);
        //Lấy hình ảnh chuyển vào thư mục hình ảnh 
        var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
        //Nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
        if (System.IO.File.Exists(path))
        {
          ViewBag.upload = "Hình đã tồn tại";
          return View();
        }
        else
        {
          //Lấy hình ảnh đưa vào thư mục HinhAnhSP
          HinhAnh[0].SaveAs(path);
          sp.HinhAnh = fileName;
        }
      }
      cf.ExecuteNonQuery(string.Format("exec stored_ThemSanPham @TenSP = N'{0}', " +
             "@NgayCapNhat = '{1}', @DonGia = {2}, @MoTa = {3}, @HinhAnh = {4}, " +
             "@SoLuongTon = {5}, @LuotXem = {6}, @LuotBinhChon = {7}, @MaLoaiSP = {8}, @DaXoa = 0, " +
             "@NgayDang = '{9}', @MaSuKien = {10} ", sp.TenSP, sp.NgayCapNhat, sp.DonGia, sp.MoTa, sp.HinhAnh,
             sp.SoLuongTon, sp.LuotXem, sp.LuotBinhChon, sp.MaLoaiSP, sp.NgayDang, sp.MaSuKien));
      //db.SANPHAMs.Add(sp);
      //db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult ChinhSua(int? id)
    {
      if (id == null)
      {
        Response.StatusCode = 404;
        return null;
      }
      DataTable dtSanPham = cf.ExecuteQuery(String.Format("select * from SanPham where MaSP = {0}", id));
      SANPHAM sp = null;
      foreach (DataRow dr in dtSanPham.Rows)
      {
        sp = new SANPHAM(dr);
      }
      if (sp == null)
      {
        return HttpNotFound();
      }
      DataTable dtSuKien = cf.ExecuteQuery("select * from SUKIEN");
      DataTable dtLoaiSP = cf.ExecuteQuery("select * from LOAISANPHAM");
      List<LOAISANPHAM> lstLSP = new List<LOAISANPHAM>();
      LOAISANPHAM lsp = null;
      foreach (DataRow item in dtLoaiSP.Rows)
      {
        lsp = new LOAISANPHAM(item);
        lstLSP.Add(lsp);
      }
      List<SUKIEN> lstLSK = new List<SUKIEN>();
      SUKIEN SuKien = null;
      foreach (DataRow item in dtSuKien.Rows)
      {
        SuKien = new SUKIEN(item);
        lstLSK.Add(SuKien);
      }
      ViewBag.MaSuKien = new SelectList(lstLSK, "MaSuKien", "TenSuKien", sp.MaSuKien);
      ViewBag.MaLoaiSP = new SelectList(lstLSP, "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
      return View(sp);
    }
    [ValidateInput(false)]
    [HttpPost]
    public ActionResult ChinhSua(SANPHAM model, HttpPostedFileBase[] HinhAnh)
    {
      DataTable dtSuKien = cf.ExecuteQuery("select * from SUKIEN");
      DataTable dtLoaiSP = cf.ExecuteQuery("select * from LOAISANPHAM");
      List<LOAISANPHAM> lstLSP = new List<LOAISANPHAM>();
      LOAISANPHAM lsp = null;
      foreach (DataRow item in dtLoaiSP.Rows)
      {
        lsp = new LOAISANPHAM(item);
        lstLSP.Add(lsp);
      }
      List<SUKIEN> lstLSK = new List<SUKIEN>();
      SUKIEN SuKien = null;
      foreach (DataRow item in dtSuKien.Rows)
      {
        SuKien = new SUKIEN(item);
        lstLSK.Add(SuKien);
      }
      ViewBag.MaSuKien = new SelectList(lstLSK, "MaSuKien", "TenSuKien", model.MaSuKien);
      ViewBag.MaLoaiSP = new SelectList(lstLSP, "MaLoaiSP", "TenLoai", model.MaLoaiSP);
      int loi = 0;
      if (HinhAnh[0] != null)
      {
        //Kiểm tra nội dung hình ảnh
        if (HinhAnh[0].ContentLength > 0)
        {
          //Kiểm tra định dạng hình ảnh
          if (HinhAnh[0].ContentType != "image/jpeg" && HinhAnh[0].ContentType != "image/png" && HinhAnh[0].ContentType != "image/gif" && HinhAnh[0].ContentType != "image/jpg")
          {
            ViewBag.upload += "Hình ảnh" + " không hợp lệ <br />";
            loi++;
          }
          else
          {
            //Kiểm tra hình ảnh tồn tại
            //Lấy tên hình ảnh
            var fileName = Path.GetFileName(HinhAnh[0].FileName);
            //Lấy hình ảnh chuyển vào thư mục hình ảnh 
            var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
            //Nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
            if (System.IO.File.Exists(path))
            {
              ViewBag.upload1 = "Hình " + "đã tồn tại <br />";
              loi++;
            }
            else
            {
              HinhAnh[0].SaveAs(path);
              model.HinhAnh = fileName;
            }
          }
        }
      }
      if (loi > 0)
      {
        return View(model);
      }
      cf.ExecuteNonQuery(string.Format("exec stored_SuaSanPham @MaSP={0},@TenSP = N'{1}', " +
             "@NgayCapNhat = '{2}', @DonGia = {3}, @MoTa = {4}, @HinhAnh = '{5}', " +
             "@SoLuongTon = {6}, @LuotXem = {7}, @LuotBinhChon = {8}, @MaLoaiSP = {9}, " +
             "@NgayDang = '{10}', @MaSuKien = {11} ", model.MaSP, model.TenSP, model.NgayCapNhat, model.DonGia,
             model.MoTa, model.HinhAnh, model.SoLuongTon, model.LuotXem, model.LuotBinhChon, model.MaLoaiSP,
             model.NgayDang, model.MaSuKien));
      return RedirectToAction("Index");
    }
    public ActionResult XoaTamThoiSanPham(int? maSP)
    {
      if (maSP == null)
      {
        Response.StatusCode = 404;
        return null;
      }
      DataTable dtSanPham = cf.ExecuteQuery(String.Format("select * from SanPham where MaSP = {0}", maSP));
      SANPHAM sp = null;
      foreach (DataRow dr in dtSanPham.Rows)
      {
        sp = new SANPHAM(dr);
      }
      if (sp == null)
      {
        return HttpNotFound();
      }
      if (cf.ExecuteNonQuery("exec stored_XoaTamThoiSanPham @MaSP=" + maSP) > 0)
      {
        TempData["result"] = "Xóa sản phẩm tạm thời thành công!";
        return RedirectToAction("Index");
      }
      TempData["error"] = "Xóa sản phẩm tạm thời thất bại!";
      return RedirectToAction("Index");
    }
    public ActionResult XoaHoanToanSanPham(int? maSP)
    {
      if (maSP == null)
      {
        Response.StatusCode = 404;
        return null;
      }
      DataTable dtSanPham = cf.ExecuteQuery(String.Format("select * from SanPham where MaSP = {0}", maSP));
      SANPHAM sp = null;
      foreach (DataRow dr in dtSanPham.Rows)
      {
        sp = new SANPHAM(dr);
      }
      if (sp == null)
      {
        return HttpNotFound();
      }
      if (cf.ExecuteNonQuery("exec XoaSP @MaSP=" + maSP) > 0)
      {
        TempData["result"] = "Xóa sản phẩm vĩnh viễn thành công!";
        return RedirectToAction("Index");
      }
      TempData["error"] = "Xóa sản phẩm vĩnh viễn thất bại!";
      return RedirectToAction("Index");
    }
  }
}