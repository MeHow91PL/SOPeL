using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class PacjenciController : Controller
    {
        // GET: Pacjenci
        public ActionResult Index()
        {
            return View("~/Views/Przychodnia/Pacjenci/Index.cshtml");
        }
    }
}