using SOPeL.Controllers;
using System.Web.Mvc;

namespace SOPeL.Infrastructure
{
    public class PortalPacjentaMasterController : AccountController
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/PortalPacjenta/_SidebarPortal.cshtml");
        }
    }
}