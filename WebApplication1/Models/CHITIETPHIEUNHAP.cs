using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebApplication1.Models
{
    public class CHITIETPHIEUNHAP
    {
        public int MaPN { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }

        public virtual PHIEUNHAP PHIEUNHAP { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}