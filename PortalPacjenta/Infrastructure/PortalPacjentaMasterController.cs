using PortalPacjenta.Controllers;
using System.Web.Mvc;

namespace PortalPacjenta.Infrastructure
{
    public class PortalPacjentaMasterController : AccountController
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/PortalPacjenta/_SidebarPortal.cshtml");
        }
    }
}