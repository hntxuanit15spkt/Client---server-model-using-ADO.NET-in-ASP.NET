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
        Config cf = new Config(Connect.ConnectString);
        public ActionResult Index()
        {
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
        public ActionResult ThemNguoiDung()
        {
            if (cf.Connection())
            {
                //Load dropdownlist Mã Loại người dùng
                DataTable dtLoaiND = cf.ExecuteQuery("select * from LOAINGUOIDUNG");
                List<LOAINGUOIDUNG> lstLND = new List<LOAINGUOIDUNG>();
                LOAINGUOIDUNG lnd = null;
                foreach (DataRow item in dtLoaiND.Rows)
                {
                    lnd = new LOAINGUOIDUNG(item);
                    lstLND.Add(lnd);
                }              

                ViewBag.MaLoaiNguoiDung = new SelectList(lstLND.OrderBy(n=>n.MaLoaiNguoiDung), "MaLoaiNguoiDung", "TenLoaiNguoiDung");
                return View();
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index", "QuanLyNguoiDung");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemNguoiDung(NGUOIDUNG nd)
        {
            //Load dropdownlist Mã Loại người dùng
            DataTable dtLoaiND = cf.ExecuteQuery("select * from LOAINGUOIDUNG");
            List<LOAINGUOIDUNG> lstLND = new List<LOAINGUOIDUNG>();
            LOAINGUOIDUNG lnd = null;
            foreach (DataRow item in dtLoaiND.Rows)
            {
                lnd = new LOAINGUOIDUNG(item);
                lstLND.Add(lnd);
            }

            ViewBag.MaLoaiNguoiDung = new SelectList(lstLND.OrderBy(n => n.MaLoaiNguoiDung), "MaLoaiNguoiDung", "TenLoaiNguoiDung");

            cf.ExecuteNonQuery(string.Format("exec stored_ThemNguoiDung @Ho = N'{0}', " +
                      "@TenLot = N'{1}', @Ten = N'{2}', @GioiTinh = {3}, @DiaChi = {4}, " +
                      "@SoDienThoai = {5}, @Email = {6}, @MaLoaiNguoiDung = {7}, @TaiKhoan = {8}, @MatKhau = {9}, " +
                      "@TrangThai = 0", nd.Ho,nd.TenLot, nd.Ten, nd.GioiTinh, nd.DiaChi, nd.SoDienThoai,
                      nd.Email, nd.MaLoaiNguoiDung,nd.TaiKhoan, nd.MatKhau));

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CapNhatThongTinNguoiDung(int? id)
        {
            DataTable dt1 = cf.ExecuteQuery(string.Format("select * from NGUOIDUNG where MaNguoiDung={0}", id));
            List<NGUOIDUNG> listND = new List<NGUOIDUNG>();
            NGUOIDUNG nd = null;
            foreach (DataRow dr in dt1.Rows)
            {
                nd = new NGUOIDUNG(dr);
                listND.Add(nd);
            }
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (nd == null)
            {
                return HttpNotFound();
            }
            DataTable dtLoaiND = cf.ExecuteQuery("select * from LOAINGUOIDUNG");
            List<LOAINGUOIDUNG> lstLND = new List<LOAINGUOIDUNG>();
            LOAINGUOIDUNG lnd = null;
            foreach (DataRow item in dtLoaiND.Rows)
            {
                lnd = new LOAINGUOIDUNG(item);
                lstLND.Add(lnd);
            }

            ViewBag.MaLoaiNguoiDung = new SelectList(lstLND.OrderBy(n => n.MaLoaiNguoiDung), "MaLoaiNguoiDung", "TenLoaiNguoiDung");
            return View(nd);
        }
        [ValidateInput(false)]
        [HttpPost]

        //string Ho, string TenLot, string Ten, bool GioiTinh, string DiaChi,
        //string SoDienThoai, int MaNguoiDung, string Email, int MaLoaiNguoiDung, string TaiKhoan, string MatKhau, bool TrangThai
        public ActionResult CapNhatThongTinNguoiDung(NGUOIDUNG model)
        {
            DataTable dtLoaiND = cf.ExecuteQuery("select * from LOAINGUOIDUNG");
            List<LOAINGUOIDUNG> lstLND = new List<LOAINGUOIDUNG>();
            LOAINGUOIDUNG lnd = null;
            foreach (DataRow item in dtLoaiND.Rows)
            {
                lnd = new LOAINGUOIDUNG(item);
                lstLND.Add(lnd);
            }

            ViewBag.MaLoaiNguoiDung = new SelectList(lstLND.OrderBy(n => n.MaLoaiNguoiDung), "MaLoaiNguoiDung", "TenLoaiNguoiDung");
            cf.ExecuteNonQuery("EXEC dbo.store_CapNhatThongTinNguoiDung"  
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
         
            return RedirectToAction("Index");            
        }

        public ActionResult DanhSachNguoiDungTheoLoai(int MaLoaiNguoiDung/*, int? page*/)
        {
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
    }
}