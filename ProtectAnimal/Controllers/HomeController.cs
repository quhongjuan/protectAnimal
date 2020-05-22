using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtectAnimal.Models;
using ProtectAnimal.oprate;

namespace ProtectAnimal.Controllers
{
    public class HomeController : MyController
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//防止重复提交
        public ActionResult Register2(Models.user u)
        {

            AjaxResult ajaxResult = new AjaxResult();
            if (ModelState.IsValid)//通过校验
            {

                if (Op_User.Add(u))
                {
                    ajaxResult.Result = DoResult.Success;
                    ajaxResult.PromptMsg = "注册成功";
                    //修改到登录
                    return RedirectToAction("index");
                }
                else
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "注册失败,请重试";
                }
            }
            else
            {
                ajaxResult.Result = DoResult.Failed;
                ajaxResult.PromptMsg = "输入错误：未填全或者格式错误";
            }
            return Json(ajaxResult);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginEmail, string loginPassword)
        {
            AjaxResult ajaxResult = new AjaxResult();
            if (loginEmail == "" || loginPassword == "")
            {
                ajaxResult.Result = DoResult.Failed;
                ajaxResult.PromptMsg = "请将信息填全！！";
            }
            else
            {
                user u = new user();
                u.Email = loginEmail;
                u.Password = loginPassword;
                user findUser = Op_User.FindUser(u);
                if (findUser != null)
                {
                    ajaxResult.Result = DoResult.Success;
                    ajaxResult.PromptMsg = "登录成功";
                    //设置登录状态
                    Session["USER"] = findUser;
                }
                else
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "邮箱或者密码不对";
                }
            }
            return Json(ajaxResult);
        }

        [HttpGet]
        public ActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Change(string email,string oldPassword,string newPassword)
        {
            AjaxResult ajaxResult = new AjaxResult();
            if (email == "" || oldPassword == ""|| newPassword=="")
            {
                ajaxResult.Result = DoResult.Failed;
                ajaxResult.PromptMsg = "请将信息填全！！";
            }
            else
            {
                user u = (user)Session["USER"];
                if (u.Email == email && u.Password == oldPassword)
                {
                    if (Op_User.changePass(u, newPassword))
                    {
                        Session.Abandon();
                        ajaxResult.Result = DoResult.Success;
                        ajaxResult.PromptMsg = "修改成功，请重新登录";
                    }
                    else
                    {
                        ajaxResult.Result = DoResult.Failed;
                        ajaxResult.PromptMsg = "修改失败，请重试";
                    }
                }
                else
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "邮箱或者原密码不对";
                }
               
            }
            return Json(ajaxResult);
        }
         
        public ActionResult Exit()
        {
            Session.Abandon();
            return Redirect("/Home/login");
        }
        

    }
}
 