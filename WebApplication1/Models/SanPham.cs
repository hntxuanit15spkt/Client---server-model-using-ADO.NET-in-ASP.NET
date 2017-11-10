using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
  public class SanPham
  {
    public int MaSP { get; set; }

    [StringLength(300)]
    public string TenSP { get; set; }

    public DateTime  NgayCapNhat { get; set; }

    public decimal   DonGia { get; set; }

    public string Mota { get; set; }

    public string Hinhanh { get; set; }

    public int SoLuongton { get; set; }

    public int LuotXem { get; set; }

    public int LuotBinhChon { get; set; }

    public int MaLoaiSP { get; set; }

    public bool DaXoa { get; set; }

    public DateTime NgayDang { get; set; }

    public int MaSukien { get; set; }

  }
}