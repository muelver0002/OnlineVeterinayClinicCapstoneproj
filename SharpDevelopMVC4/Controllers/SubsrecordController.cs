using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	
	public class SubsrecordController : Controller
	{   
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		[Authorize]
		public ActionResult Index()
		{
			List<Subsrecord> subs = _db.Subsrecord.OrderByDescending(o => o.Id).ToList();
			return View(subs);
		}
		
		
		
		
		[Authorize]
		public ActionResult Duedate()
		{
			var date = DateTime.Now.Date;
			List<Subsrecord> subs = _db.Subsrecord.Where(x => DateTime.Today >= x.Duedate).OrderByDescending(o => o.Id).ToList();
			return View(subs);
		}
		
		[Authorize]
		[HttpGet]
		public ActionResult Create(int Id)
		{   
			ViewBag.id = Id;
			Payment payment = _db.Payments.Find(Id);
			return View(payment);
		}
		[Authorize]
		[HttpPost]
		public ActionResult Create(Subsrecord subs, int Id, DateTime? Duedate)
		{		
			_db.Subsrecord.Add(subs);
			_db.SaveChanges();
			
			var Payment = _db.Payments.Find(Id);
			Payment.Duedate = Duedate;
			_db.Entry(Payment).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			TempData["subbmgs"] ="Subscription Successfully Saved..";
			return RedirectToAction("Index");
		}
		[Authorize]
		public ActionResult Delete(int Id)
		{
			var Payment = _db.Subsrecord.Find(Id);
			if(Payment != null)
			{
				_db.Subsrecord.Remove(Payment);
				_db.SaveChanges();
			}
			
			return RedirectToAction("Index");		
		}
		
		
		[Authorize]
		public ActionResult Deletedue(int Id)
		{
			var Payment = _db.Subsrecord.Find(Id);
			if(Payment != null)
			{
				_db.Subsrecord.Remove(Payment);
				_db.SaveChanges();
			}
			
			return RedirectToAction("Duedate");
		
		
		}
		
	}
}