using SOPeL.Controllers;
using System.Web.Mvc;

namespace SOPeL.Infrastructure
{
    public class PrzychodniaMasterController : AccountController
    {
        public PartialViewResult getSidebar()
        {
            return PartialView("~/Views/Przychodnia/_SidebarPrzychodnia.cshtml");
        }
    }
}