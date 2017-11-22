using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
  public class SANPHAMKHACHHANGDAT
  {
    public SANPHAMKHACHHANGDAT()
    {

    }
    public SANPHAMKHACHHANGDAT(DataRow red)
    {
      this.MaSP = Convert.ToInt32(red["MaSP"]);
      if (!Convert.IsDBNull(red["TenSP"]))
        this.TenSP = red["TenSP"].ToString();
      this.HinhAnh = red["HinhAnh"].ToString();
      if (!Convert.IsDBNull(red["DonGia"]))
        this.DonGia = Convert.ToDecimal(red["DonGia"]);
      if (!Convert.IsDBNull(red["SoLuong"]))
        this.SoLuongDaDatCuaKH = Convert.ToInt32(red["SoLuong"]);
      if (!Convert.IsDBNull(red["ThoiDiemDat"]))
        this.ThoiDiemDatCuaKH = Convert.ToDateTime(red["ThoiDiemDat"]);
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
    public int SoLuongDaDatCuaKH { get; set; }
    public DateTime ThoiDiemDatCuaKH { get; set; }
    public virtual LOAISANPHAM LOAISANPHAM { get; set; }
    public virtual SUKIEN SUKIEN { get; set; }
    public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
  }
}