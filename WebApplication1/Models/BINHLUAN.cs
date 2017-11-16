//--using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class BINHLUAN
    {
        public int MaBL { get; set; }
        public string NoiDungBL { get; set; }
        public int MaSP { get; set; }
        public int MaNguoiDungKhachHang { get; set; }
        public DateTime ThoiDiem { get; set; }
    
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
