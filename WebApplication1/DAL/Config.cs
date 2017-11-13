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
        private string ConnectString = "";
        protected SqlConnection connect = new SqlConnection();
        protected SqlCommand cmd = new SqlCommand();
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
        private SqlConnection openConnection()
        {
            if (connect.State == ConnectionState.Closed || connect.State == ConnectionState.Broken)
            {
                connect = new SqlConnection(ConnectString);
                connect.Open();
            }
            return connect;
        }
        public void Insert(string sql)
        {
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        public void Delete(string sql)
        {
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        public void Update(string sql)
        {
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        //Tham số thứ 2 (parameter) không điền vào cũng được
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            data.Clear();
            connect = openConnection();
            SqlCommand command = new SqlCommand(query, connect);
            if (parameter != null)
            {
                string[] listParam = query.Split(' ');
                int i = 0;
                foreach (string item in listParam)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            data = ds.Tables[0];
            return data;
        }
        //public DataSet ExecuteQuery(string strSQL, CommandType ct)
        //{
        //    if (connect.State == ConnectionState.Open)
        //        connect.Close();
        //    connect.Open();
        //    cmd.CommandText = strSQL;
        //    cmd.CommandType = ct;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    return ds;
        //}
        //Trả về số dòng bị ảnh hưởng, thường dùng trong các stored
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            connect = openConnection();
            SqlCommand command = new SqlCommand(query, connect);
            if (parameter != null)
            {
                string[] listParam = query.Split(' ');
                int i = 0;
                foreach (string item in listParam)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }
            data = command.ExecuteNonQuery();
            return data;
        }
        //Trả về cột đầu tiên của dòng đầu tiên, thường dùng cho các function
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            connect = openConnection();
            SqlCommand command = new SqlCommand(query, connect);
            if (parameter != null)
            {
                string[] listParam = query.Split(' ');
                int i = 0;
                foreach (string item in listParam)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }
            data = command.ExecuteScalar();
            return data;
        }
        #region Tham khảo thêm
        //public void Insert(SanPham sp)
        //{
        //  cmd = new SqlCommand();
        //  cmd.Connection = connect;
        //  cmd.CommandText = "Insert into sanpham values (@tensp,'5/6/2017',@dongia,@mota,@hinhanh,@Soluongton,@Luotxem,@luong,@maloaisp,@daxoa,'5/6/2017',@mask)";
        //  cmd.Parameters.AddWithValue("@tensp", sp.TenSP ?? "");
        //  // cmd.Parameters.AddWithValue("@ngaycapnhat", sp.NgayCapNhat);
        //  cmd.Parameters.AddWithValue("@dongia", sp.DonGia);
        //  cmd.Parameters.AddWithValue("@mota", sp.Mota ?? "");
        //  cmd.Parameters.AddWithValue("@hinhanh", sp.Hinhanh);
        //  cmd.Parameters.AddWithValue("@Soluongton", sp.SoLuongton);
        //  cmd.Parameters.AddWithValue("@Luotxem", sp.LuotXem);
        //  cmd.Parameters.AddWithValue("@luong", sp.LuotBinhChon);
        //  cmd.Parameters.AddWithValue("@maloaisp", sp.MaLoaiSP);
        //  cmd.Parameters.AddWithValue("@daxoa", sp.DaXoa);
        //  cmd.Parameters.AddWithValue("@mask", sp.MaSukien);
        //  cmd.ExecuteNonQuery();
        //}

        //public List<SanPham> ListAll(string sql)
        //{
        //  cmd = new SqlCommand();
        //  cmd.Connection = connect;
        //  cmd.CommandText = sql;
        //  SqlDataReader red = cmd.ExecuteReader();
        //  var listsp = new List<SanPham>();
        //  while (red.Read())
        //  {
        //    SanPham sp = new SanPham();
        //    sp.MaSP = Convert.ToInt32(red["MaSP"]);
        //    if (!Convert.IsDBNull(red["TenSP"]))
        //      sp.TenSP = red["TenSP"].ToString();
        //    if (!Convert.IsDBNull(red["NgayCapNhat"]))
        //      sp.NgayCapNhat = Convert.ToDateTime(red["NgayCapNhat"]);
        //    if (!Convert.IsDBNull(red["DonGia"]))
        //      sp.DonGia = Convert.ToDecimal(red["DonGia"]);
        //    sp.Mota = red["MoTa"].ToString();
        //    sp.Hinhanh = red["HinhAnh"].ToString();
        //    if (!Convert.IsDBNull(red["SoLuongTon"]))
        //      sp.SoLuongton = Convert.ToInt32(red["SoLuongTon"]);
        //    if (!Convert.IsDBNull(red["LuotXem"]))
        //      sp.LuotXem = Convert.ToInt32(red["LuotXem"]);
        //    if (!Convert.IsDBNull(red["LuotBinhChon"]))
        //      sp.LuotBinhChon = Convert.ToInt32(red["LuotBinhChon"]);
        //    if (!Convert.IsDBNull(red["MaLoaiSP"]))
        //      sp.MaLoaiSP = Convert.ToInt32(red["MaLoaiSP"]);
        //    if (!Convert.IsDBNull(red["DaXoa"]))
        //      sp.DaXoa = Convert.ToBoolean(red["DaXoa"]);
        //    if (!Convert.IsDBNull(red["NgayDang"]))
        //      sp.NgayDang = Convert.ToDateTime(red["NgayDang"]);
        //    if (!Convert.IsDBNull(red["MaSuKien"]))
        //      sp.MaSukien = Convert.ToInt32(red["MaSuKien"]);
        //    listsp.Add(sp);
        //  }
        //  red.Close();
        //  connect.Close();
        //  return listsp;
        //}
        #endregion
    }
}