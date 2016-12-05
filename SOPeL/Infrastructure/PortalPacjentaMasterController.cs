using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Infrastructure
{
    public class PortalPacjentaMasterController : Controller
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/PortalPacjenta/_SidebarPortal.cshtml");
        }
    }
}