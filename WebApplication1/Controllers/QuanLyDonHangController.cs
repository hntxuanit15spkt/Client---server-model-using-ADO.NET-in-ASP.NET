using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DAL;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        Config cf = new Config(Connect.ConnectString);
        public ActionResult Index()
        {
            DataTable dt1 = cf.ExecuteQuery("select * from DONDATHANG");
            List<DONDATHANG> listDDH = new List<DONDATHANG>();
            DONDATHANG ddh = null;
            foreach (DataRow dr in dt1.Rows)
            {
                ddh = new DONDATHANG(dr);
                listDDH.Add(ddh);
            }
            return View(listDDH.OrderByDescending(n => n.MaDDH));
        }
       
        public ActionResult DuyetDonHang(int? MaDDH, int ? MaNV)
        {
            if (MaDDH == null || MaNV == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DataTable dtDDH = cf.ExecuteQuery(String.Format("select * from DONDATHANG where MaDDH = {0}", MaDDH));
            DONDATHANG ddh = null;
            foreach (DataRow dr in dtDDH.Rows)
            {
                ddh = new DONDATHANG(dr);
            }
            if (ddh == null)
            {
                return HttpNotFound();
            }
            if (ddh.MaNV == 0 )
            {
                if (cf.ExecuteNonQuery(string.Format("exec store_DuyetDonHang @MaDDH={0}", MaDDH)) > 0)
                {
                    TempData["result"] = "Duyệt đơn hàng thành công!";
                    return RedirectToAction("Index");
                }
                TempData["error"] = "Duyệt đơn hàng thất bại!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult XemThem(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DataTable dtDDH = cf.ExecuteQuery(String.Format("select * from DONDATHANG where MaDDH = {0}", id));
            DONDATHANG ddh = null;
            foreach (DataRow dr in dtDDH.Rows)
            {
                ddh = new DONDATHANG(dr);
            }
            if (ddh == null)
            {
                return HttpNotFound();
            }            
            return View(ddh);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XemThem(DONDATHANG model)
        {        
            cf.ExecuteNonQuery("SELECT *FROM dbo.thongtinDDH(" + model.MaDDH + ")");
            return RedirectToAction("Index");
        }

    }
}