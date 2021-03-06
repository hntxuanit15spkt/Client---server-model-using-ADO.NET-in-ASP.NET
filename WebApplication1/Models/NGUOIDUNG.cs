﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
  public class NGUOIDUNG
  {

        public NGUOIDUNG(DataRow red)
        {
            this.MaNguoiDung = Convert.ToInt32(red["MaNguoiDung"]);
            if (!Convert.IsDBNull(red["Ho"]))
                this.Ho = red["Ho"].ToString();
            if (!Convert.IsDBNull(red["TenLot"]))
                this.TenLot = red["TenLot"].ToString();
            if (!Convert.IsDBNull(red["Ten"]))
                this.Ten = red["Ten"].ToString();
            if (!Convert.IsDBNull(red["GioiTinh"]))
                this.GioiTinh = Convert.ToBoolean(red["GioiTinh"]);
            if (!Convert.IsDBNull(red["DiaChi"]))
                this.DiaChi = red["DiaChi"].ToString();
            if (!Convert.IsDBNull(red["SoDienThoai"]))
                this.SoDienThoai = red["SoDienThoai"].ToString();
            if (!Convert.IsDBNull(red["Email"]))
                this.Email = red["Email"].ToString();
            if (!Convert.IsDBNull(red["MaLoaiNguoiDung"]))
                this.MaLoaiNguoiDung = Convert.ToInt32(red["MaLoaiNguoiDung"]);
            if (!Convert.IsDBNull(red["TaiKhoan"]))
                this.TaiKhoan = red["TaiKhoan"].ToString();
            if (!Convert.IsDBNull(red["MatKhau"]))
                this.MatKhau = red["MatKhau"].ToString();           
            if (!Convert.IsDBNull(red["TrangThai"]))
                this.TrangThai = Convert.ToBoolean(red["TrangThai"]);
        }
   

    public string Ho { get; set; }
    public string TenLot { get; set; }
    public string Ten { get; set; }
    public bool GioiTinh { get; set; }
    public string DiaChi { get; set; }
    public string SoDienThoai { get; set; }
    public int MaNguoiDung { get; set; }
    public string Email { get; set; }
    public int MaLoaiNguoiDung { get; set; }
    public string TaiKhoan { get; set; }
    public string MatKhau { get; set; }
    public bool TrangThai { get; set; }
    public virtual ICollection<GIOHANG> GIOHANGs { get; set; }
    public virtual LOAINGUOIDUNG LOAINGUOIDUNG { get; set; }
    public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    public virtual ICollection<QUYENHANNGUOIDUNG> QUYENHANNGUOIDUNGs { get; set; }
  }
}
