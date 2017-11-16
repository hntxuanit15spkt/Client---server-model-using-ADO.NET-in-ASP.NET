using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DAL;

namespace WebApplication1.Helper
{
     public static class Connect
     {
          //biến này sẽ lưu chuỗi kết nối gloal,coi như mình lưu vào webconfig đó
          public static string ipaddress = "";
          public static string databasename="";
          public static string username = "";
          public static string password = "";
          public static string ConnectString = "";
          public static bool CheckConnection()
          {
               Config cf = new Config(Connect.ConnectString);
               return cf.Connection();
          }
     }
}