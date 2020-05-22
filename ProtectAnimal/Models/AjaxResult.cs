using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtectAnimal.Models
{
    public class AjaxResult
    {
        public AjaxResult() { }
        
        //提示信息
        public string PromptMsg { get; set; }
        //返回成功与否
        public DoResult Result { get; set; }
        //返回类型
        public object RetValue { get; set; }
    }
    public enum DoResult
    {
        Failed=0,
        Success=1,
        OverTime=2,
        UnSession=3
    }
}