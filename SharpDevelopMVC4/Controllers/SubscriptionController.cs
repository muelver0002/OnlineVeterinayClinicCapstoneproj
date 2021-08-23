using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	/// <summary>
	/// Description of SubscriptionController.
	/// </summary>
	public class SubscriptionController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		public ActionResult Index()
		{
			return View();
		}
		[HttpGet]
		[Authorize]
		public ActionResult Createsub()
		{
			return View();
		}
		[HttpPost]
		[Authorize]
		public ActionResult Createsub(Subscription subscriptions)
		{
			_db.Subscriptions.Add(subscriptions);
			_db.SaveChanges();
			return View();
		}
		
		[HttpGet]
		[Authorize]
		public ActionResult Editsub(int? Id)
		{
			return View();
		}
		
		[HttpPost]
		[Authorize]
		public ActionResult Editsub(Subscription subscription)
		{
			return View();
		}
		
		[Authorize]
		public ActionResult Deletesub()
		{
			return View();
		}
	}
}