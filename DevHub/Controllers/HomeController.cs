using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace DevHub.Controllers
{

   
    
    public class HomeController : Controller
    {
        ProfileBLL _profileBLL = new ProfileBLL();
       
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Developer")]
        [Route("dashboard")]
        public ActionResult Dashboard()
        {
            AccountBLL accountBLL = new AccountBLL();
           Guid id =  Guid.Parse(Request.Cookies["UserData"]["Id"]);
            return View(accountBLL.DeveloperDetails(id));
        }

        public JsonResult DeleteEducation(string id)
        {
           
            return Json(_profileBLL.DeleteEducation(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteExperience(string id)
        {
            return Json(_profileBLL.DeleteExperience(id), JsonRequestBehavior.AllowGet);
        }



    }
}