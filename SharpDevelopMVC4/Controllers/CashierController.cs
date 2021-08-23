using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;


namespace SharpDevelopMVC4.Controllers
{

	public class CashierController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		public ActionResult Index()
		{
			return View();
		}
		
		
		
		
		public ActionResult Compute()
		{
			return View();
		}
		
//		[HttpPost]
//		public ActionResult Compute()
//		{
//			
//			return View();
//		}
	}
}