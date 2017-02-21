using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using PagarMe;

namespace PagarmeAsp2.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			return View();
		}

		public ActionResult Capture()
		{
			var amount = 1000;
			var token = Request.Form.Get("token");
			Transaction tx = PagarMeService.GetDefaultService().Transactions.Find(token);
			try
			{
				tx.Capture(amount);
				ViewData["Message"] = "Success";
			}
			catch (Exception e)
			{
				var x = e.StackTrace;
				ViewData["Message"] = x;
			}

			return View();
		}
	}
}
