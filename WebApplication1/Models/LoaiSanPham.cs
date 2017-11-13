using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LOAISANPHAM
    {

        public LOAISANPHAM(DataRow item)
        {

            this.MaLoaiSP = (int)item["MaLoaiSP"];
            this.TenLoai = item["TenLoai"].ToString();
        }

        public int MaLoaiSP { get; set; }
        public string TenLoai { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}