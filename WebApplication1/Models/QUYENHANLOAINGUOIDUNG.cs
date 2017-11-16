using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class QUYENHANLOAINGUOIDUNG
    {
        public int MaLoaiNguoiDung { get; set; }
        public int MaChucNang { get; set; }
        public string GhiChu { get; set; }
    
        public virtual CHUCNANG CHUCNANG { get; set; }
        public virtual LOAINGUOIDUNG LOAINGUOIDUNG { get; set; }
    }
}
