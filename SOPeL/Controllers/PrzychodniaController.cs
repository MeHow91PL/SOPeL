using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
	public class PrzychodniaController : Controller
	{
		//
		// GET: /Przychodnia/
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult pobierzModulAdmin(string wybranyModul)
		{
			string modul = wybranyModul;
            string path = "~/Views/Przychodnia/" + modul + "/Index.cshtml";
			return PartialView(path);
		}
	}
}