using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	/// <summary>
	/// Description of ChangepassController.
	/// </summary>
	public class ChangepassController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}