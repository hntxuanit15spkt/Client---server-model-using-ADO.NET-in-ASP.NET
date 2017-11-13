using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LOAISANPHAM
    {
        public int MaLoaiSP { get; set; }
        public string TenLoai { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}