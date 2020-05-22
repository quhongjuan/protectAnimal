using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProtectAnimal.Models;

namespace ProtectAnimal.oprate
{
    public class Op_Organization
    {
        //添加组织
        public static bool Add(organization organ)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                string name = organ.oname;
                string time = organ.time;
                string creator = organ.creator;
                string desc = organ.description;
                string task = organ.task;
                string contact = organ.contact;

                string mySql = "INSERT INTO organization(oname,time,creator,description,task,contact) VALUES(@name,@time,@creator,@desc,@task,@contact)";
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);

                MySqlParameter p1 = new MySqlParameter("@name", MySqlDbType.VarChar);
                p1.Value = name;
                MySqlParameter p2 = new MySqlParameter("@time", MySqlDbType.VarChar);
                p2.Value = time;
                MySqlParameter p3 = new MySqlParameter("@creator", MySqlDbType.VarChar);
                p3.Value = creator;
                MySqlParameter p4 = new MySqlParameter("@desc", MySqlDbType.VarChar);
                p4.Value = desc;
                MySqlParameter p5 = new MySqlParameter("@task", MySqlDbType.VarChar);
                p5.Value = task;
                MySqlParameter p6 = new MySqlParameter("@contact", MySqlDbType.VarChar);
                p6.Value = contact;

                sqlcmd2.Parameters.Add(p1); sqlcmd2.Parameters.Add(p2);
                sqlcmd2.Parameters.Add(p3); sqlcmd2.Parameters.Add(p4);
                sqlcmd2.Parameters.Add(p5); sqlcmd2.Parameters.Add(p6);
                
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
        //查询组织
        public static List<organization> FindAllOrgan()
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                string mySql = "select * from organization order by oid desc";

                List<organization> organs = new List<organization>();

                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlDataReader sqlreader = sqlcmd2.ExecuteReader();
                while (sqlreader.Read())
                {
                    //txtName.Text = sqlreader["列名"].ToString();
                    
                    organization or = new organization();
                    or.oname = sqlreader["oname"].ToString();
                    or.time = sqlreader["time"].ToString();
                    or.creator = sqlreader["creator"].ToString();
                    or.description = sqlreader["description"].ToString();
                    or.task = sqlreader["task"].ToString();
                    or.contact = sqlreader["contact"].ToString();

                    organs.Add(or);
                }
                return organs;
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
    }
}