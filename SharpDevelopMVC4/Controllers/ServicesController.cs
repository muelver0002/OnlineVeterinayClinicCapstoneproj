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
	/// Description of ServicesController.
	/// </summary>
	public class ServicesController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
			
			var username = Session["user"].ToString();
			var adds = _db.Vetowners.Where(x => x.Username == username).FirstOrDefault();		
			
			int user = adds.Id;
			
			List<Servicesacon> service = _db.Servicesacons.Where(x => x.VetId == user).ToList();
			
			return View(service);   
			}
			
			return RedirectToAction("Logoff", "Account");
		}
		
		
		
		public ActionResult Create()
		{
		    
			return View();
		
		}
		[HttpPost]
		public ActionResult Create(Servicesacon service)
		{
		
			
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var VetId = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
				int Id = VetId.Id;
				
				service.VetId = Id;
				
				TempData["servicetmsg"] ="text";
				
			  _db.Servicesacons.Add(service);
			  _db.SaveChanges();
			  return View();
			
			
			}
			return RedirectToAction("Logoff", "Account");
		
		}
		[Authorize]
		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var service = _db.Servicesacons.Find(Id);
			if(service != null)
			{
				ViewBag.Id = Id;
				
				return View(service);
			  
			
			}
			return RedirectToAction("Index");
		
		}
		[Authorize]
		[HttpPost]
		public ActionResult Edit(Servicesacon Service)
		{
			var services = _db.Servicesacons.Find(Service.Id);
			
			services.Servicesname = Service.Servicesname;
			services.Price = Service.Price;
			
			_db.Entry(services).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			
			return RedirectToAction("Index");
		
		}
		[Authorize]
		public ActionResult Delete(int Id)
		{  
			
			var service = _db.Servicesacons.Find(Id);
			if(service != null)
			{
				_db.Servicesacons.Remove(service);
				_db.SaveChanges();
			
			}
		    
			return RedirectToAction("Index");
		}
		
		
	}
}