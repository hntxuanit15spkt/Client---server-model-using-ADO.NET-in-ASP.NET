﻿//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Data;
//using System.Linq;
//using System.Web;
//using WebApplication1.Models;

//namespace WebApplication1.Models
//{
//  public class ItemGioHang
//    {
//        public int MaSP { get; set; }
//        public string TenSP { get; set; }
//        public int SoLuong { get; set; }
//        public decimal DonGia { get; set; }
//        public decimal ThanhTien { get; set; }
//        public string HinhAnh { get; set; }
//        public ItemGioHang(int iMaSP)
//        {
//            //using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
//            //{
//            //    this.MaSP = iMaSP;
//            //    SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
//            //    this.TenSP = sp.TenSP;
//            //    this.HinhAnh = sp.HinhAnh;
//            //    this.DonGia = sp.DonGia.Value;
//            //    this.SoLuong = 1;
//            //    this.ThanhTien = DonGia * SoLuong;
//            //}
//        }
//        public ItemGioHang(int iMaSP, int sl)
//        {
//            //using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
//            //{
//            //    this.MaSP = iMaSP;
//            //    SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
//            //    this.TenSP = sp.TenSP;
//            //    this.HinhAnh = sp.HinhAnh;
//            //    this.DonGia = sp.DonGia.Value;
//            //    this.SoLuong = sl;
//            //    this.ThanhTien = DonGia * SoLuong;
//            //}
//        }
//        public ItemGioHang()
//        {


//        }

//    }
//}