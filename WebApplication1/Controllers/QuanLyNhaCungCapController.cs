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
    public class QuanLyNhaCungCapController : Controller
    {
        // GET: QuanLyNhaCungCap
        Config cf = new Config(Connect.ConnectString);
        public ActionResult Index()
        {
            DataTable dt1 = cf.ExecuteQuery("select * from NHACUNGCAP");
            List<NHACUNGCAP> listNCC = new List<NHACUNGCAP>();
            NHACUNGCAP nnc = null;
            foreach (DataRow dr in dt1.Rows)
            {
                nnc = new NHACUNGCAP(dr);
                listNCC.Add(nnc);
            }
            return View(listNCC.OrderByDescending(n => n.MaNCC));
        }

    }
}