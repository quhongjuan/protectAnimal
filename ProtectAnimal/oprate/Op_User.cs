using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProtectAnimal.Models;

namespace ProtectAnimal.oprate
{
    public class Op_User
    {
        //添加用户
        public static bool Add(user u)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                
                string mySql = "INSERT INTO user(Name,Email,Password) VALUES(@name,@email,@password)";
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);

                MySqlParameter p1 = new MySqlParameter("@name", MySqlDbType.VarChar);
                p1.Value = u.Name;
                MySqlParameter p2 = new MySqlParameter("@email", MySqlDbType.VarChar);
                p2.Value = u.Email;
                MySqlParameter p3 = new MySqlParameter("@password", MySqlDbType.VarChar);
                p3.Value = u.Password;

                sqlcmd2.Parameters.Add(p1);
                sqlcmd2.Parameters.Add(p2);
                sqlcmd2.Parameters.Add(p3); 

                int iResult = sqlcmd2.ExecuteNonQuery();
                if (iResult > 0)
                    return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return false;
        }
        //查询用户
        public static user FindUser(user u)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                string e = u.Email;
                string p = u.Password;
                string mySql = "select * from user where Email=@email and Password=@pass";
                
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlParameter p1 = new MySqlParameter("@email", MySqlDbType.VarChar);
                p1.Value = e;
                MySqlParameter p2 = new MySqlParameter("@pass", MySqlDbType.VarChar);
                p2.Value = p;
                sqlcmd2.Parameters.Add(p1);
                sqlcmd2.Parameters.Add(p2);
                user temp = new user();
                MySqlDataReader sqlreader = sqlcmd2.ExecuteReader();
                if (sqlreader.Read())
                {
                    //txtName.Text = sqlreader["列名"].ToString();
                    
                    temp.Uid = Convert.ToInt32(sqlreader["Uid"]);
                    temp.Name = sqlreader["Name"].ToString();
                    temp.Email = sqlreader["Email"].ToString();
                    temp.Password = sqlreader["Password"].ToString();
                    return temp;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return null;
        }
        //修改密码
        public static bool changePass(user u,string pass)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                string e = u.Email;
                string p = pass;
                string mySql = "update user set Password=@password where Email=@email ";

                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlParameter p1 = new MySqlParameter("@password", MySqlDbType.VarChar);
                p1.Value = p;
                MySqlParameter p2 = new MySqlParameter("@email", MySqlDbType.VarChar);
                p2.Value = e;
                sqlcmd2.Parameters.Add(p1);
                sqlcmd2.Parameters.Add(p2);
                int iResult = sqlcmd2.ExecuteNonQuery();
                if (iResult > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return false;
        }
    }
}