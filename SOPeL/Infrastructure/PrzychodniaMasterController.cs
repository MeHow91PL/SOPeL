using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Infrastructure
{
    public class PrzychodniaMasterController : Controller
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/Przychodnia/_SidebarPrzychodnia.cshtml");
        }
    }
}