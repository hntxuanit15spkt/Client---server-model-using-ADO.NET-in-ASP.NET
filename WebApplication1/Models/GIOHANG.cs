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
  public class GIOHANG
  {
    public GIOHANG(DataRow dataRow)
    {
      this.MaGioHang = (int)dataRow["MaGioHang"];
      this.MaKH = (int)dataRow["MaKH"];
      this.DaDat = (bool)dataRow["DaDat"];
      if(!Convert.IsDBNull(dataRow["ThanhTien"]))
      { 
        this.ThanhTien = (decimal)dataRow["ThanhTien"];
      }
      else
      {
        this.ThanhTien = 0;
      }
    }
    public int MaGioHang { get; set; }
    public int MaKH { get; set; }
    public decimal ThanhTien { get; set; }
    public bool DaDat { get; set; }
    public virtual ICollection<CHITIETGIOHANG> CHITIETGIOHANGs { get; set; }
    public virtual ICollection<DONDATHANG> DONDATHANGs { get; set; }
    public virtual LOAINGUOIDUNG NGUOIDUNG { get; set; }
  }
}
