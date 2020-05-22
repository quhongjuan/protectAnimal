using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtectAnimal.Models;
using ProtectAnimal.oprate;

namespace ProtectAnimal.Controllers
{
    public class ArticleController : MyController
    {
        // GET: Article
        
        public ActionResult ShowContext(int id)
        {
            List<works> workList = new List<works>();
            workList = Op_Works.FindArticleByFlag(id);
            return View(workList);
        }
        public ActionResult ShowDetail(int wid)
        {
            works w = Op_Works.FindArticleByWid(wid);
            return View(w);
        }
        public ActionResult CatPersonArt()
        {
           
            user u = isLogin();
            List<works> worksList = new List<works>();
            if (u != null)//已登录
                worksList = Op_Works.FindArticleByUid(u.Uid);
            return View(worksList);
            


        }
        [HttpGet]
        public ActionResult PublishArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PublishArticle(string title, string coreSentences, string flag, string context)
        {
            
            AjaxResult ajaxResult = new AjaxResult();
            //判断是否登录
            if (isLogin() == null)
            {
                ajaxResult.Result = DoResult.UnSession;
                ajaxResult.PromptMsg = "还未登录，请先登录！";
            }
            else
            {
                if (title == "" || coreSentences == "" || flag == "" || context == "")
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "请将所有信息添加完整！";
                }
                else
                {
                    works work = new works();
                    user u = (user)Session["USER"];
                    int uid = u.Uid; string name = u.Name;
                    work.Uid = uid; work.Flag = int.Parse(flag); work.Context = context;
                    work.Title = title; work.CoreSentence = coreSentences; work.author = name;
                    //保存图片及路径
                    string imagePath = Request.MapPath("~/Uppictures/" + Request.Files["image"].FileName);
                    Request.Files["image"].SaveAs(imagePath);
             
                     work.Image = "/Uppictures/" + Request.Files["image"].FileName;
                    //work.Image = imagePath;//会拒绝访问，因为绝对路径访问属于访问外部资源

                    System.Diagnostics.Debug.Write("自定义错误："+work.ToString());
                    if (Op_Works.Add(work))
                    {
                        ajaxResult.Result = DoResult.Success;
                        ajaxResult.PromptMsg = "上传成功";
                        ajaxResult.RetValue = int.Parse(flag);
                    }
                    else
                    {
                        ajaxResult.Result = DoResult.Failed;
                        ajaxResult.PromptMsg = "上传失败,请重试";
                    }
                }
            }

            
           return Json(ajaxResult);
        }

        public ActionResult CatAuthorArt(int uid)
        {
            List<works> worksList = new List<works>();
            worksList = Op_Works.FindArticleByUid(uid);
            return View(worksList);
        }
        public ActionResult DeleteArticle(int wid)
        {
            Op_Works.DeleteArticle(wid);
            return Redirect("/Article/CatPersonArt");
        }
    }
}