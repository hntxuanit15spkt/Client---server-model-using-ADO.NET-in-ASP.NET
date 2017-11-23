using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
namespace WebApplication1.Models
{
  public  class NHACUNGCAP
    {
        public NHACUNGCAP(DataRow red)
        {
            this.MaNCC = Convert.ToInt32(red["MaNCC"]);
            if (!Convert.IsDBNull(red["TenNCC"]))
                this.TenNCC = red["TenNCC"].ToString();
            if (!Convert.IsDBNull(red["DiaChi"]))
                this.DiaChi = red["DiaChi"].ToString();
            if (!Convert.IsDBNull(red["Email"]))
                this.Email = red["Email"].ToString();
            if (!Convert.IsDBNull(red["SoDienThoai"]))
                this.SoDienThoai = red["SoDienThoai"].ToString();
            if (!Convert.IsDBNull(red["Fax"]))
                this.Fax = red["Fax"].ToString();
        }
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string Fax { get; set; }
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    }
}
