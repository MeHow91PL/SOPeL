using SOPeL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class WizytaController : PrzychodniaMasterController
    {
        // GET: Wizyta
        public ActionResult Index()
        {
            return View();
        }
    }
}