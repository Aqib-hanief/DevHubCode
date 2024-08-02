using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;
using System.Web.Security;
using ModelLayer.ViewModels;

namespace DevHub.Controllers
{
    public class AccountController : Controller
    {
        AccountBLL _accountBLL = new AccountBLL();
        // GET: Account
        [Route("signup")]
        public ActionResult SignUp()
        {
            if (User.IsInRole("Developer"))
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegisterVM registerVM)
        {

            if (ModelState.IsValid)
            {
                ViewBag.Message = _accountBLL.CreateAccount(registerVM, "Developer");
                if (ViewBag.Message == "")
                {
                    return RedirectToAction("ConfirmAccount");
                }
            }
            return View();
        }


        public ActionResult ConfirmAccount()
        {

            return View();
        }
        [HttpGet]
        public ActionResult ConfirmUser()
        {
            String guid = Request.QueryString["guid"];
            if (string.IsNullOrEmpty(guid))
            {
                ModelState.AddModelError("error", "Invalid Request");
                return RedirectToAction("Error");
            }
            int returnValue = _accountBLL.ConfirmUser(guid);
            if (returnValue == -1)
            {
                ModelState.AddModelError("error", "Invalid Request");
                return RedirectToAction("Error");
            }
            return RedirectToAction("Login");
        }


        [Route("login")]
        public ActionResult Login()
        {
            if (User.IsInRole("Developer"))
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                Account account = _accountBLL.Login(loginVM);
                if (account == null)
                {
                    ViewBag.Message = "Invalid Credentials";
                    return View();
                }
                else if (account.IsActive == false)
                {
                    ViewBag.Message = "Your account is either blocked or not confirmed";
                    return View();
                }
                HttpCookie cookie = new HttpCookie("UserData");
                cookie["Id"] = account.Id.ToString();
                cookie["Name"] = account.Name;
                cookie["Email"] = account.Email;
                cookie["ImagePath"] = account.ImagePath;
                cookie["UserRole"] = account.UserRole;


                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, account.Email, DateTime.Now, DateTime.Now.AddDays(10), loginVM.IsChecked, FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookiee = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent)
                {
                    cookiee.Expires = ticket.Expiration;
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookiee);
                Response.Cookies.Add(cookie);
                if (account.UserRole == "Developer")
                {
                    return RedirectToAction("Dashboard", "Home");
                }

            }

            return View();
        }


        public ActionResult Error()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Route("forgetpassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            ViewBag.Message = _accountBLL.ForgetPassword(email);
            return View();
        }

        [HttpGet]
        [Route("resetpassword")]
        public ActionResult ResetPassword(string guid, string email)
        {
            ViewBag.IsConfirmed =  _accountBLL.IsConfirmedForPasswordChange(guid, email);
          if (ViewBag.IsConfirmed == true)
            {
                ViewBag.Email = email;
                ViewBag.Guid = guid;
               
            }
            return View();
        }

        
        [Route("resetpassword")]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
              
                ViewBag.Message = _accountBLL.ResetPassword(resetPasswordVM);
            }
           
           
            return View();
        }



    }
}