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
    using WebApplication1.Models;

    public partial class CHITIETGIOHANG
    {
        public int MaGioHang { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
    
        public virtual GIOHANG GIOHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
