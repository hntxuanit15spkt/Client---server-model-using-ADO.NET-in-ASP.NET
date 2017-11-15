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
            Connect.ConnectString = " Data Source=" + ip + ";Initial Catalog=" + Databasename + ";Integrated Security=False;User ID=" + usr + ";Password=" + pwd;
            // Config cf = new Config(Connect.ConnectString);
            //var list = new List<SanPham>();
            //var check = cf.Connection();
            if (Connect.CheckConnection())
            {
                TempData["result"] = "Kết nối thành công!";
                //list = cf.ListAll("select * from SanPham where DaXoa=0");
            }
            else
            {
                TempData["result"] = "Kết nối cơ sở dữ liệu không thành công!";
                return RedirectToAction("Index", "test");
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
        public ActionResult MenuPartial()
        {
            //var lstSP = db.SanPhams;
            Config cf = new Config(Connect.ConnectString);
            List<LOAISANPHAM> listLSP = new List<LOAISANPHAM>();
            LOAISANPHAM lsp = null;
            DataTable dtSP = cf.ExecuteQuery("select * from LOAISANPHAM");
            foreach (DataRow item in dtSP.Rows)
            {
                lsp = new LOAISANPHAM(item);
                listLSP.Add(lsp);
            }
            return PartialView(listLSP);
        }
        //public ActionResult MenuPartial()
        //{
        //    //var lstSP = db.SanPhams;
        //    Config cf = new Config(Connect.ConnectString);
        //    List<SANPHAM> listSP = new List<SANPHAM>();
        //    DataTable dtSP = cf.ExecuteQuery("select * from SanPham");
        //    foreach (DataRow item in dtSP.Rows)
        //    {
        //        SANPHAM sp = new SANPHAM(item);
        //        listSP.Add(sp);
        //    }
        //    return PartialView(listSP);
        //}
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}