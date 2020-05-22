using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtectAnimal.Models;

namespace ProtectAnimal.Controllers
{
    public class MyController: Controller
    {
        public const int Asia = 0;
        public const int Europe = 1;
        public const int Africa = 2;
        public const int North = 3;
        public const int South = 4;
        public const int Oceania = 5;
        public const int ArcticOcean = 6;
        public const int Antarctica = 7;
        //Session["USER"]=null;

        public  user isLogin()
        {
            //首先判断用户是否登录
            if (Session["USER"] == null)
            {
                return null;
            }
            else
            {
                user u = (user) Session["USER"];
                return u;
            }
        }
    }
}