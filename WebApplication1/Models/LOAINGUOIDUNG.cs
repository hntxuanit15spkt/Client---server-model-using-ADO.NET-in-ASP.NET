//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public class LOAINGUOIDUNG
    {
        public int MaLoaiNguoiDung { get; set; }
        public string TenLoaiNguoiDung { get; set; }
        public virtual ICollection<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual ICollection<QUYENHANLOAINGUOIDUNG> QUYENHANLOAINGUOIDUNGs { get; set; }
    }
}