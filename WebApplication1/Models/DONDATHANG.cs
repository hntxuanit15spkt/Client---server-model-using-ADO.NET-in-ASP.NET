using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class DONDATHANG
    {
        public int MaDDH { get; set; }
        public DateTime ThoiDiemDat { get; set; }
        public int TinhTrangGiaoHang { get; set; }
        public DateTime ThoiDiemLap { get; set; }
        public DateTime NgayGiaoDuKien { get; set; }
        public int UuDai { get; set; }
        public decimal TongTien { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public int MaGioHang { get; set; }
        public decimal PhiVanChuyen { get; set; }
        public virtual ICollection<CHITIETDONDATHANG> CHITIETDONDATHANGs { get; set; }
        public virtual GIOHANG GIOHANG { get; set; }
    }
}
