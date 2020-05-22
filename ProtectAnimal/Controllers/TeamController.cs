using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtectAnimal.Models;
using ProtectAnimal.oprate;

namespace ProtectAnimal.Controllers
{
    public class TeamController : MyController
    {
        [HttpGet]
        public ActionResult RegisterTeam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterTeam(string oname,string time,string creator,string desc,string task,string contact)
        {
            AjaxResult ajaxResult = new AjaxResult();

            organization or = new organization();
            or.oname = oname;
            or.time = time;
            or.creator = creator;
            or.description = desc;
            or.task = task;
            or.contact = contact;
            if(Op_Organization.Add(or))
            {
                ajaxResult.Result = DoResult.Success;
                ajaxResult.PromptMsg = "注册成功";
            }
            else
            {
                ajaxResult.Result = DoResult.Failed;
                ajaxResult.PromptMsg = "注册失败";
            }
            return Json(ajaxResult);
        }
        public ActionResult CatTeam()
        {
            List<organization> AllOrgan = new List<organization>();
            AllOrgan = Op_Organization.FindAllOrgan();
            return View(AllOrgan);
        }
        //test
        [HttpGet]
        public ActionResult MyTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyTest(string name,string image)
        {
            
            string imagePath = Request.MapPath("~/Uppictures/" + Request.Files["image"].FileName);
            Request.Files["image"].SaveAs(imagePath);
           
            /*
            int flags = int.Parse(flag);
            return Content(name+" "+flags);
            */
           
            return Content(imagePath);
        }
        
    }
}