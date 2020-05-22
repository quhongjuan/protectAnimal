using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProtectAnimal.Models;

namespace ProtectAnimal.oprate
{
    public class Op_Works
    {
        //添加作品
        public static bool Add(works work)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                int uid = work.Uid;
                int  flag = work.Flag;
                string context = work.Context;
                string image = work.Image;
                string title = work.Title;
                string core = work.CoreSentence;
                string author = work.author;

                string mySql = "INSERT INTO works(Uid,Flag,Context,Image,Title,CoreSentence,author) VALUES(@uid,@flag,@context,@image,@title,@core,@author)";
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);

                MySqlParameter p1 = new MySqlParameter("@uid", MySqlDbType.Int32);
                p1.Value = uid;
                MySqlParameter p2 = new MySqlParameter("@flag", MySqlDbType.Int32);
                p2.Value = flag;
                MySqlParameter p3 = new MySqlParameter("@context", MySqlDbType.Text);
                p3.Value = context;
                MySqlParameter p4 = new MySqlParameter("@image", MySqlDbType.VarChar);
                p4.Value = image;
                MySqlParameter p5 = new MySqlParameter("@title", MySqlDbType.VarChar);
                p5.Value = title;
                MySqlParameter p6 = new MySqlParameter("@core", MySqlDbType.VarChar);
                p6.Value = core;
                MySqlParameter p7 = new MySqlParameter("@author", MySqlDbType.VarChar);
                p7.Value = author;

                sqlcmd2.Parameters.Add(p1);
                sqlcmd2.Parameters.Add(p2);
                sqlcmd2.Parameters.Add(p3);
                sqlcmd2.Parameters.Add(p4);
                sqlcmd2.Parameters.Add(p5);
                sqlcmd2.Parameters.Add(p6);
                sqlcmd2.Parameters.Add(p7);
                

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
        //根据flag查询作品
        public static List<works> FindArticleByFlag(int id)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                string mySql = "select * from works where Flag=@id order by Wid desc";
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlParameter p1 = new MySqlParameter("@id", MySqlDbType.Int32);
                p1.Value = id;
                sqlcmd2.Parameters.Add(p1); ;
                List<works> worksList = new List<works>();
                
                MySqlDataReader sqlreader = sqlcmd2.ExecuteReader();
                while (sqlreader.Read())
                {
                    //txtName.Text = sqlreader["列名"].ToString();

                    works work = new works();
                    work.Wid = Convert.ToInt32(sqlreader["Wid"]);
                    work.Uid = Convert.ToInt32(sqlreader["Uid"]);
                    work.Flag = Convert.ToInt32(sqlreader["Flag"]);
                    work.Context = sqlreader["Context"].ToString();
                    work.Image = sqlreader["Image"].ToString();
                    work.Title = sqlreader["Title"].ToString();
                    work.CoreSentence = sqlreader["CoreSentence"].ToString();
                    work.author = sqlreader["author"].ToString();
                    worksList.Add(work);
                }
                return worksList;
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
        //根据作品编号查询作品
        public static works FindArticleByWid(int id)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                
                string mySql = "select * from works where Wid=@wid";

                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlParameter p1 = new MySqlParameter("@wid", MySqlDbType.Int32);
                p1.Value = id;
              
                sqlcmd2.Parameters.Add(p1);
               
                works temp = new works();
                MySqlDataReader sqlreader = sqlcmd2.ExecuteReader();
                if (sqlreader.Read())
                {
                    //txtName.Text = sqlreader["列名"].ToString();
                    temp.Wid = Convert.ToInt32(sqlreader["Wid"]);
                    temp.Uid = Convert.ToInt32(sqlreader["Uid"]);
                    temp.Flag = Convert.ToInt32(sqlreader["Flag"]);
                    temp.Context = sqlreader["Context"].ToString();
                    temp.Image = sqlreader["Image"].ToString();
                    temp.Title = sqlreader["Title"].ToString();
                    temp.CoreSentence = sqlreader["CoreSentence"].ToString();
                    temp.author = sqlreader["author"].ToString();
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
        //根据作者查找作品
        public static List<works> FindArticleByUid(int id)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();

                string mySql = "select * from works where Uid=@uid";

                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);
                MySqlParameter p1 = new MySqlParameter("@uid", MySqlDbType.Int32);
                p1.Value = id;

                sqlcmd2.Parameters.Add(p1);

                List<works> workList = new List<works>();
                MySqlDataReader sqlreader = sqlcmd2.ExecuteReader();
                while(sqlreader.Read())
                {
                    //txtName.Text = sqlreader["列名"].ToString();
                    works temp = new works();
                    temp.Wid = Convert.ToInt32(sqlreader["Wid"]);
                    temp.Uid = Convert.ToInt32(sqlreader["Uid"]);
                    temp.Flag = Convert.ToInt32(sqlreader["Flag"]);
                    temp.Context = sqlreader["Context"].ToString();
                    temp.Image = sqlreader["Image"].ToString();
                    temp.Title = sqlreader["Title"].ToString();
                    temp.CoreSentence = sqlreader["CoreSentence"].ToString();
                    temp.author = sqlreader["author"].ToString();

                    workList.Add(temp);
                }
                return workList;
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
        //根据作品号删除作品
        public static bool DeleteArticle(int wid)
        {
            //string constr= ConfigurationManager.ConnectionStrings["qhj_aspEntities"].ConnectionString;
            string constr = "Server=localhost;UserId=root;Password=qhj12345;Database=qhj_asp;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
               
                string mySql = "delete from works where Wid=@wid";
                MySqlCommand sqlcmd2 = new MySqlCommand(mySql, conn);

                MySqlParameter p1 = new MySqlParameter("@wid", MySqlDbType.Int32);
                p1.Value = wid;
                sqlcmd2.Parameters.Add(p1);
                
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
    }
    

}