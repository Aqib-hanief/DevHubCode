using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevHub.Areas.AdminArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: AdminArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}