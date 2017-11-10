using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.DAL
{
  public class DataProvider
  {
    //string ConnStr = "Data Source=USERMIC-J9AE653\\XUAN;Initial Catalog=QuanLyBanHang;Integrated Security=True";
    //SqlConnection conn = null;
    SqlCommand comm = null;
    SqlDataAdapter da = null;
    public DataProvider()
    {
      //conn = new SqlConnection(ConnStr);
      //comm = conn.CreateCommand();
    }
    //Đây là hàm mà em dự tính dùng nó để truyền vào chuỗi kết nối vào và mở kết nối lên
    public bool ConnectToDB(string strSQL,SqlConnection conn)
    {
      try
      {
        conn = new SqlConnection(strSQL);
        if (conn.State == ConnectionState.Open)
        { 
          conn.Close();
        }
        conn.Open();
        return true;
      }
      catch
      {

      }
      finally
      {
        conn.Close();
      }
      return false;
    }
  //  public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)//thực thi truy vấn và trả về một DataSet
  //  {
  //    if (conn.State == ConnectionState.Open)
  //      conn.Close();
  //    conn.Open();
  //    comm.CommandText = strSQL;
  //    comm.CommandType = ct;
  //    da = new SqlDataAdapter(comm);
  //    DataSet ds = new DataSet();
  //    da.Fill(ds);
  //    return ds;
  //  }
  //  public int MyExecuteScalar(string strSQL, CommandType ct, ref string error)
  //  {
  //    int k = 0;
  //    if (conn.State == ConnectionState.Open)
  //    {
  //      conn.Close();
  //    }
  //    conn.Open();
  //    comm.CommandText = strSQL;
  //    comm.CommandType = ct;
  //    try
  //    {
  //      k = Int32.Parse(comm.ExecuteScalar().ToString());
  //    }
  //    catch (SqlException e)
  //    {
  //      error = e.Message;
  //    }
  //    return k;
  //  }

  //  public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error)
  //  {
  //    bool f = false;
  //    if (conn.State == ConnectionState.Open)
  //      conn.Close();
  //    conn.Open();
  //    comm.CommandText = strSQL;
  //    comm.CommandType = ct;
  //    try
  //    {
  //      comm.ExecuteNonQuery();
  //      f = true;
  //    }
  //    catch (SqlException ex)
  //    {
  //      error = ex.Message;
  //    }
  //    finally
  //    {
  //      conn.Close();
  //    }
  //    return f;
  //  }
  }
}