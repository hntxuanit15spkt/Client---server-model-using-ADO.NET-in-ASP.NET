using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.DAL;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class CHITIETGIOHANG
  {

    public CHITIETGIOHANG(DataRow dataRow)
    {
      Config cf = new Config(Connect.ConnectString);
      this.MaGioHang = (int)dataRow["MaGioHang"];
      this.MaSP = (int)dataRow["MaSP"];
      DataTable dt = cf.ExecuteQuery("select * from SANPHAM where MaSP=" + this.MaSP);
      SANPHAM sp = new SANPHAM(dt.Rows[0]);
      this.SoLuong = (int)dataRow["SoLuong"];
      this.HinhAnh = sp.HinhAnh;
      this.TenSP = sp.TenSP;
      this.DonGia = sp.DonGia;
      this.ThanhTien = this.SoLuong * this.DonGia;
    }
    public int MaGioHang { get; set; }
    public int MaSP { get; set; }
    public int SoLuong { get; set; }
    public string HinhAnh { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }
    public string TenSP { get; set; }
    public virtual GIOHANG GIOHANG { get; set; }
    public virtual SANPHAM SANPHAM { get; set; }
  }
}
