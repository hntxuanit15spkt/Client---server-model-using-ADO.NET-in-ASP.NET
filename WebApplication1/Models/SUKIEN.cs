using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SUKIEN
    {
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