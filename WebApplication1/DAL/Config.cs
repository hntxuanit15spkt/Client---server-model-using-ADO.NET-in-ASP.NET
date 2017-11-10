using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
  public class Config
  {
    //đây là lớp cha,bạn có thể kế thừa nó và sử dụng cá biến proteced
    //Data Source=DESKTOP-2RV2058;Initial Catalog=QuanLyBanHang123;Integrated Security=True
    private string ConnectString = "";
    protected SqlConnection connect;
    protected SqlCommand cmd;
    public Config(string connectstr)
    {
      this.ConnectString = connectstr;
    }
    public bool Connection()
    {
      try
      {
        connect = new SqlConnection(ConnectString);
        connect.Open();
        return true;
      }
      catch
      {
        return false;
      }

    }
    public List<SanPham> ListAll(string sql)
    {
      cmd = new SqlCommand();
      cmd.Connection = connect;
      cmd.CommandText = sql;
      SqlDataReader red = cmd.ExecuteReader();
      var listsp = new List<SanPham>();
      while (red.Read())
      {
        SanPham sp = new SanPham();
        sp.MaSP = Convert.ToInt32(red["MaSP"]);
        if (!Convert.IsDBNull(red["TenSP"]))
          sp.TenSP = red["TenSP"].ToString();
        if (!Convert.IsDBNull(red["NgayCapNhat"]))
          sp.NgayCapNhat = Convert.ToDateTime(red["NgayCapNhat"]);
        if (!Convert.IsDBNull(red["DonGia"]))
          sp.DonGia = Convert.ToDecimal(red["DonGia"]);
        sp.Mota = red["MoTa"].ToString();
        sp.Hinhanh = red["HinhAnh"].ToString();
        if (!Convert.IsDBNull(red["SoLuongTon"]))
          sp.SoLuongton = Convert.ToInt32(red["SoLuongTon"]);
        if (!Convert.IsDBNull(red["LuotXem"]))
          sp.LuotXem = Convert.ToInt32(red["LuotXem"]);
        if (!Convert.IsDBNull(red["LuotBinhChon"]))
          sp.LuotBinhChon = Convert.ToInt32(red["LuotBinhChon"]);
        if (!Convert.IsDBNull(red["MaLoaiSP"]))
          sp.MaLoaiSP = Convert.ToInt32(red["MaLoaiSP"]);
        if (!Convert.IsDBNull(red["DaXoa"]))
          sp.DaXoa = Convert.ToBoolean(red["DaXoa"]);
        if (!Convert.IsDBNull(red["NgayDang"]))
          sp.NgayDang = Convert.ToDateTime(red["NgayDang"]);
        if (!Convert.IsDBNull(red["MaSuKien"]))
          sp.MaSukien = Convert.ToInt32(red["MaSuKien"]);
        listsp.Add(sp);
      }
      red.Close();
      connect.Close();
      return listsp;
    }

    public void Delete(int id)
    {
      cmd = new SqlCommand();
      cmd.Connection = connect;
      cmd.CommandText = "Update Sanpham set DaXoa=1 where MaSP=@id";
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
    }

    public void Update(string sql)
    {
      cmd = new SqlCommand();
      cmd.Connection = connect;
      cmd.CommandText = sql;
      cmd.ExecuteNonQuery();
    }
    public void Insert(SanPham sp)
    {
      cmd = new SqlCommand();
      cmd.Connection = connect;
      cmd.CommandText = "Insert into sanpham values (@tensp,'5/6/2017',@dongia,@mota,@hinhanh,@Soluongton,@Luotxem,@luong,@maloaisp,@daxoa,'5/6/2017',@mask)";
      cmd.Parameters.AddWithValue("@tensp", sp.TenSP ?? "");
      // cmd.Parameters.AddWithValue("@ngaycapnhat", sp.NgayCapNhat);
      cmd.Parameters.AddWithValue("@dongia", sp.DonGia);
      cmd.Parameters.AddWithValue("@mota", sp.Mota ?? "");
      cmd.Parameters.AddWithValue("@hinhanh", sp.Hinhanh);
      cmd.Parameters.AddWithValue("@Soluongton", sp.SoLuongton);
      cmd.Parameters.AddWithValue("@Luotxem", sp.LuotXem);
      cmd.Parameters.AddWithValue("@luong", sp.LuotBinhChon);
      cmd.Parameters.AddWithValue("@maloaisp", sp.MaLoaiSP);
      cmd.Parameters.AddWithValue("@daxoa", sp.DaXoa);
      cmd.Parameters.AddWithValue("@mask", sp.MaSukien);
      cmd.ExecuteNonQuery();
    }
  }
}