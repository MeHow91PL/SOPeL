using PortalPacjenta.Controllers;
using System.Web.Mvc;

namespace PortalPacjenta.Infrastructure
{
    public class PrzychodniaMasterController : AccountController
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/Przychodnia/_SidebarPrzychodnia.cshtml");
        }
    }
}