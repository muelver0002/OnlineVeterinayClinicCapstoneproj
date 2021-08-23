using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	
	public class AddressController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		public ActionResult Index()
		{
			List<Address> address = _db.Addreses.ToList();
			
			return View(address);
		}
		
		public ActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Add(Address address)
		{
			_db.Addreses.Add(address);
			_db.SaveChanges();
			return View();
		}
	}
}