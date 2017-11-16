using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class PHIEUNHAP
    {
        public int MaPN { get; set; }
        public int MaNCC { get; set; }
        public DateTime ThoiDiemNhap { get; set; }
        public int MaNV { get; set; }
        public decimal TongChiPhi { get; set; }
        public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
    }
}
