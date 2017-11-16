using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class NGUOIDUNG
    {
        public string Ho { get; set; }
        public string TenLot { get; set; }
        public string Ten { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int MaNguoiDung { get; set; }
        public string Email { get; set; }
        public int MaLoaiNguoiDung { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public bool TrangThai { get; set; }
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }
        public virtual LOAINGUOIDUNG LOAINGUOIDUNG { get; set; }
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual ICollection<QUYENHANNGUOIDUNG> QUYENHANNGUOIDUNGs { get; set; }
    }
}
