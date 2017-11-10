using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlHelper
    {
        private static SqlHelper instance = null;
        public static SqlHelper Intance { get => (instance == null) ? new SqlHelper() : instance; }
        private static SqlConnection conn;
        //private static string connectionString = @"Data Source=.\;Initial Catalog=quanlysinhvien2;Integrated Security=True";
        private static string connectionString = @"Data Source=(local);Initial Catalog=QuanLyThuVien;Integrated Security=True";
        public SqlHelper()
        {
            conn = new SqlConnection(connectionString);
        }
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            conn = openConnection();
            SqlCommand command = new SqlCommand(query, conn);
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
            adapter.Fill(data);
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            conn = openConnection();
            SqlCommand command = new SqlCommand(query, conn);
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

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            conn = openConnection();

            SqlCommand command = new SqlCommand(query, conn);
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
    }
}
