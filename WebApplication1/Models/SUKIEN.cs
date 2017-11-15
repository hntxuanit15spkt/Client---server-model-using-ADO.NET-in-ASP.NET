using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
  public class SUKIEN
  {

    public SUKIEN(DataRow item)
    {
      this.MaSuKien = (int)item["MaSuKien"];
      this.TenSuKien = item["TenSuKien"].ToString();
      this.MoTa = item["MoTa"].ToString();
      this.HinhAnh = item["HinhAnh"].ToString();
      this.UuDai = (int)item["UuDai"];
      this.NgayBatDau = (DateTime)item["NgayBatDau"];
      this.NgayKetThuc = (DateTime)item["NgayKetThuc"];

    }

    public int MaSuKien { get; set; }
    public string TenSuKien { get; set; }
    public string MoTa { get; set; }
    public string HinhAnh { get; set; }
    public int UuDai { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
  }
}