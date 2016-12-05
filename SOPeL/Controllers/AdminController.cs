using SOPeL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class AdminController : PrzychodniaMasterController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Przychodnia/Admin/Index.cshtml");
        }
    }
}