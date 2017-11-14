using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
  public class SANPHAM
  {
    public SANPHAM()
    {

    }
    public SANPHAM(DataRow red)
    {
      this.MaSP = Convert.ToInt32(red["MaSP"]);
      if (!Convert.IsDBNull(red["TenSP"]))
        this.TenSP = red["TenSP"].ToString();
      if (!Convert.IsDBNull(red["NgayCapNhat"]))
        this.NgayCapNhat = Convert.ToDateTime(red["NgayCapNhat"]);
      if (!Convert.IsDBNull(red["DonGia"]))
        this.DonGia = Convert.ToDecimal(red["DonGia"]);
      this.MoTa = red["MoTa"].ToString();
      this.HinhAnh = red["HinhAnh"].ToString();
      if (!Convert.IsDBNull(red["SoLuongTon"]))
        this.SoLuongTon = Convert.ToInt32(red["SoLuongTon"]);
      if (!Convert.IsDBNull(red["LuotXem"]))
        this.LuotXem = Convert.ToInt32(red["LuotXem"]);
      if (!Convert.IsDBNull(red["LuotBinhChon"]))
        this.LuotBinhChon = Convert.ToInt32(red["LuotBinhChon"]);
      if (!Convert.IsDBNull(red["MaLoaiSP"]))
        this.MaLoaiSP = Convert.ToInt32(red["MaLoaiSP"]);
      if (!Convert.IsDBNull(red["DaXoa"]))
        this.DaXoa = Convert.ToBoolean(red["DaXoa"]);
      if (!Convert.IsDBNull(red["NgayDang"]))
        this.NgayDang = Convert.ToDateTime(red["NgayDang"]);
      if (!Convert.IsDBNull(red["MaSuKien"]))
        this.MaSuKien = Convert.ToInt32(red["MaSuKien"]);
    }
    public int MaSP { get; set; }
    [StringLength(300)]
    public string TenSP { get; set; }
    public DateTime NgayCapNhat { get; set; }
    public decimal DonGia { get; set; }
    public string MoTa { get; set; }
    public string HinhAnh { get; set; }
    public int SoLuongTon { get; set; }
    public int LuotXem { get; set; }
    public int LuotBinhChon { get; set; }
    public int MaLoaiSP { get; set; }
    public bool DaXoa { get; set; }
    public DateTime NgayDang { get; set; }
    public int MaSuKien { get; set; }
    public virtual LOAISANPHAM LOAISANPHAM { get; set; }
    public virtual SUKIEN SUKIEN { get; set; }
    public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
  }
}