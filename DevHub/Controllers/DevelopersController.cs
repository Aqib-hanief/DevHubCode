using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer.ViewModels;

namespace DevHub.Controllers
{
    public class DevelopersController : Controller
    {
        // GET: Developers

        ProfileBLL _profileBLL = new ProfileBLL();
         [Route("developers")]  
        public ActionResult Developers()
        {            
            return View(_profileBLL.GetAllProfiles());
        }

        public JsonResult FilterDevelopers(string skill)
        {

            return Json(_profileBLL.FilterDevelopers(skill).Select(x => new DevelopersVM{
                AccountId = x.AccountId,
                Address = x.Address,
                Company = x.Account.Experiences.Count==0?null: x.Account.Experiences.FirstOrDefault(y => y.IsCurrentJob == true).Company,
                Imagepath = x.Account.ImagePath,
                Name = x.Account.Name,
                ProfessionalStatus = x.Profession.ProfessionalStatus,
                Skills = x.Skills
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        
    }
}