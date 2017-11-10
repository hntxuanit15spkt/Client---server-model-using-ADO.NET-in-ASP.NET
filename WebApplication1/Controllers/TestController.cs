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
  public class TestController : Controller
  {
    // GET: Test

    public TestController()
    {
    }
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Show(string ip, string Databasename, string usr, string pwd)
    {
      Connect.ConnectString = " Data Source=" + ip + ";Initial Catalog=" + Databasename + ";Integrated Security=False;User ID="+ usr + ";Password="+pwd;
      Config cf = new Config(Connect.ConnectString);
      var list = new List<SanPham>();
      var check = cf.Connection();
      if (check)
      {
        TempData["result"] = "Kết nối thành công!";
        list = cf.ListAll("select * from SanPham where DaXoa=0");
      }
      else
      {
        TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
        return RedirectToAction("Index", "test");
      }
      return View(list);
    }
    public ActionResult Delete(int id)
    {
      Config cf = new Config(Connect.ConnectString);
      var list = new List<SanPham>();
      var check = cf.Connection();
      if (check)
      {
        TempData["results"] = "Xóa thành công!";
        cf.Delete(id);

      }
      else
      {
        TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
        return RedirectToAction("Index", "test");
      }
      return RedirectToAction("Show", "test");
    }
    public ActionResult Update(SanPham sp)
    {
      Config cf = new Config(Connect.ConnectString);
      var list = new List<SanPham>();
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
    public ActionResult Insert()
    {
      Config cf = new Config(Connect.ConnectString);
      var list = new List<SanPham>();
      var check = cf.Connection();
      if (check)
      {
        SanPham sp = new SanPham();
        sp.TenSP = "XXX";
        sp.DonGia = 100;
        sp.Mota = "xxx";
        sp.Hinhanh = "Xx";
        sp.SoLuongton = 12;
        sp.LuotXem = 100;
        sp.MaLoaiSP = 1;
        sp.LuotBinhChon = 12;
        sp.DaXoa = false;
        sp.NgayDang = DateTime.Now;
        sp.MaSukien = 1;
        sp.NgayCapNhat = DateTime.Now;
        TempData["results"] = "Thêm thành công!";
        cf.Insert(sp);
      }
      else
      {
        TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
        return RedirectToAction("Index", "test");
      }
      return RedirectToAction("Show", "test");
    }
    public ActionResult DisConnect()
    {
      Connect.ConnectString = "";
      return RedirectToAction("Index", "test");
    }
    //anh ơi, cho em hỏi thường khi làm sẽ viết câu lệnh sql thế này hay gọi function stored ra để dùng ạ?
    ///ở đây mình ví dụ sửa tên sản phẩm,còn các nếu muốn sửa gì thì bạn có thể thêm vào câu truy vấn nhé
    /////ok,thường thì sẽ sử dụng nhiều thues,kết họp cả  function stored,sql,trigger nữa,bạn đã tìm hiểu về trigger chưa
    //dạ trigger em có tìm hiểu rồi anh, à e có viết được trigger rồi, chạy trên dbms thành công, vậy giờ làm sao
    //để nó thể hiện trên giao diện á anh, trên sql server em dùng raiserror á anh, vậy có cách nào thông báo bằng asp
    //không ạ???trigger nó sẽ chạy ngầm bên sql server đó bạn,cái đó sẽ chỉ làm 1 số công việc thôi,cunxng khong can hiện lên view,vì nó xử lý ngầm bên sqlserver
    //khi raiserror bạn có thể ghi log cũng dk, dạ
    //ok,
  }

}