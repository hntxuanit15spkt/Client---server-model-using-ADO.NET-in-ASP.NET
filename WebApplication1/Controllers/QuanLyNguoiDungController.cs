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
using System.Data;

namespace WebApplication1.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        // GET: NguoiDung
        public ActionResult Index()
        {
            Config cf = new Config(Connect.ConnectString);
            DataTable dt1 = cf.ExecuteQuery("select * from NGUOIDUNG");
            List<NGUOIDUNG> listND = new List<NGUOIDUNG>();
            NGUOIDUNG nd = null;
            foreach (DataRow dr in dt1.Rows)
            {
                nd = new NGUOIDUNG(dr);
                listND.Add(nd);
            }
            return View(listND.OrderByDescending(n => n.MaNguoiDung));
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(string Ho, string TenLot, string Ten, bool GioiTinh, string DiaChi,
            string SoDienThoai, string Email, int MaLoaiNguoiDung, string TaiKhoan, string MatKhau, bool TrangThai)
        {
            Config cf = new Config(Connect.ConnectString);
            var list = new List<NGUOIDUNG>();
            var check = cf.Connection();
            if (check)
            {
                TempData["results"] = "Thêm thành công!";
                cf.Insert("INSERT INTO NguoiDung VALUES(" + Ho + ", "
                  + TenLot + "," + Ten + "," + GioiTinh + "," + DiaChi + "," + SoDienThoai + "," + Email
                  + "," + MaLoaiNguoiDung + "," + TaiKhoan + "," + MatKhau + "," + TrangThai + ");");
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            Config cf = new Config(Connect.ConnectString);
            DataTable dt1 = cf.ExecuteQuery(string.Format("select * from NGUOIDUNG where MaNguoiDung={0}", id));
            List<NGUOIDUNG> listND = new List<NGUOIDUNG>();
            NGUOIDUNG nd = null;
            foreach (DataRow dr in dt1.Rows)
            {
                nd = new NGUOIDUNG(dr);
                listND.Add(nd);
            }
            //Lấy nhân viên cần chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (nd == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]

        //string Ho, string TenLot, string Ten, bool GioiTinh, string DiaChi,
        //string SoDienThoai, int MaNguoiDung, string Email, int MaLoaiNguoiDung, string TaiKhoan, string MatKhau, bool TrangThai
        public ActionResult Update(NGUOIDUNG model)
        {
            Config cf = new Config(Connect.ConnectString);
            var list = new List<NGUOIDUNG>();
            var check = cf.Connection();
            if (check)
            {
                TempData["results"] = "Cập nhật thành công!";
                cf.ExecuteNonQuery("EXEC dbo.CapNhatThongTinNV"  
                                + " @Ho = N'" + model.Ho + "',"
                                + " @TenLot = N'" +model.TenLot + "',"
                                + " @Ten = N'" + model.Ten + " ',"
                                + " @GioiTinh =" + model.GioiTinh + ","
                                + " @DiaChi = N'" +model.DiaChi +"',"
                                + " @SoDienThoai = '" + model.SoDienThoai + "',"
                                + " @MaNguoiDung = " + model.MaNguoiDung + ","
                                + " @Email = N'" + model.Email+"',"
                                + " @MaLoaiNguoiDung =" +model.MaLoaiNguoiDung +","
                                + " @TaiKhoan = '" +model.TaiKhoan+ "',"
                                + " @MatKhau = '" + model.MatKhau +"',"
                                + " @TrangThai = "+model.TrangThai);
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");            
        }

        public ActionResult DanhSachNguoiDungTheoLoai(int MaLoaiNguoiDung/*, int? page*/)
        {
            Config cf = new Config(Connect.ConnectString);
            DataTable dt = cf.ExecuteQuery("SELECT * FROM dbo.func_DanhSachNguoiDungTheoLoai("+MaLoaiNguoiDung+")");
            List<NGUOIDUNG> listND = new List<NGUOIDUNG>();
            NGUOIDUNG nd = null;
            foreach (DataRow dr in dt.Rows)
            {
                nd = new NGUOIDUNG(dr);
                listND.Add(nd);
            }
            //int PageSize = 1;//số sản phẩm trên trang
            //int PageNumber = (page ?? 1);//page không có giá trị thì PageNumber sẽ có giá trị là 1, số trang hiện tại
            ViewBag.MaLoaiNguoiDung = MaLoaiNguoiDung;
            //return View(listND.ToPagedList(PageNumber, PageSize));
            return View(listND);
        }
        public ActionResult MenuPartial()
        {
            Config cf = new Config(Connect.ConnectString);
            List<LOAINGUOIDUNG> listLND = new List<LOAINGUOIDUNG>();
            LOAINGUOIDUNG lnd = null;
            DataTable dtND = cf.ExecuteQuery("select * from LOAINGUOIDUNG");
            foreach (DataRow item in dtND.Rows)
            {
                lnd = new LOAINGUOIDUNG(item);
                listLND.Add(lnd);
            }
            return PartialView(listLND);
        }
    }
}