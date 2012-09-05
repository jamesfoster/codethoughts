﻿namespace CodeThoughts.Controllers
{
	using System.Web.Mvc;

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

			return View();
		}

		ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}