using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class CHUCNANG
    { 
        public int MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public virtual ICollection<QUYENHANNGUOIDUNG> QUYENHANNGUOIDUNGs { get; set; }
        public virtual ICollection<QUYENHANLOAINGUOIDUNG> QUYENHANLOAINGUOIDUNGs { get; set; }
    }
}
