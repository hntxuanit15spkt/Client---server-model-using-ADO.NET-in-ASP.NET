using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class LOAINGUOIDUNG
    {
        public int MaLoaiNguoiDung { get; set; }
        public string TenLoaiNguoiDung { get; set; }
        public virtual ICollection<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual ICollection<QUYENHANLOAINGUOIDUNG> QUYENHANLOAINGUOIDUNGs { get; set; }
        public LOAINGUOIDUNG(DataRow row)
        {

        }
    }
}
