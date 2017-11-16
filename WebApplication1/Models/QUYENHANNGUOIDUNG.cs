using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class QUYENHANNGUOIDUNG
    {
        public int MaChucNang { get; set; }
        public int MaNguoiDung { get; set; }
        public string GhiChu { get; set; }
    
        public virtual CHUCNANG CHUCNANG { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}
