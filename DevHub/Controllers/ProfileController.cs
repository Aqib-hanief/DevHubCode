using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;



namespace DevHub.Controllers
{
    [Authorize(Roles = "Developer")]
    public class ProfileController : Controller
    {
        

        ProfileBLL _profileBLL = new ProfileBLL();
        // GET: Profile
        [Route("editprofile")]
        public ActionResult EditProfile()
        {
            ViewBag.ProfessionalStatusId = new SelectList(_profileBLL.GetAllProfessions(), "Id", "ProfessionalStatus");
            return View(_profileBLL.GetProfileForEdit(Guid.Parse(Request.Cookies["UserData"]["Id"])));
        }
        [HttpPost]
        [Route("editprofile")]
        public ActionResult EditProfile(ProfileVM profileVM)
        {
            if (ModelState.IsValid)
            {
                string imgMessage = _profileBLL.EditProfile(profileVM, Guid.Parse(Request.Cookies["UserData"]["Id"]));
                if (imgMessage == "success")
                {
                    return RedirectToAction("Dashboard","Home");
                }
                ViewBag.Message = imgMessage;
            }
            ViewBag.ProfessionalStatusId = new SelectList(_profileBLL.GetAllProfessions(), "Id", "ProfessionalStatus");
            return View(_profileBLL.GetProfileForEdit(Guid.Parse(Request.Cookies["UserData"]["Id"])));
        }
        [Route("addexperience")]
        public ActionResult AddExperience()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddExperience(AddexperienceVM addexperienceVM)
        {
            _profileBLL.AddExperience(addexperienceVM, Guid.Parse(Request.Cookies["UserData"]["Id"]));
            return RedirectToAction("Dashboard","Home");
        }
        [Route("addeducation")]
        public ActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEducation(AddEducationVM addEducationVM)
        {
            _profileBLL.AddEducation(addEducationVM, Guid.Parse(Request.Cookies["UserData"]["Id"]));
            return RedirectToAction("Dashboard","Home");
        }
        [Route("profile")]
        [Authorize]
        public ActionResult Viewprofile(Guid id)
        {

            
            Profile profile = _profileBLL.GetProfile(id);
            if (profile == null) return RedirectToAction("EditProfile");
            if (profile.AccountId == Guid.Parse(Request.Cookies["UserData"]["Id"])) ViewBag.IsSame = true;
            return View(profile);
          
        }

        public JsonResult GetSocialLinks()
        {
            return Json(_profileBLL.GetSocialLinks(Guid.Parse(Request.Cookies["UserData"]["Id"])).Select(x=>new {Link=x.Link}), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateSocialLinks(SocialLinksVM socialLinksVM)
        {
            _profileBLL.UpdateSocialLinks(socialLinksVM, Guid.Parse(Request.Cookies["UserData"]["Id"]));
            return Json("", JsonRequestBehavior.AllowGet);

        }

        
        

    }
}