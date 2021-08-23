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
	/// Description of MedicineController.
	/// </summary>
	public class MedicineController : Controller
	{   
		SdMvc4DbContext _db = new SdMvc4DbContext();
		[Authorize]
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner"))
				{
				var user = Session["user"].ToString();
				var userinfo = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
				int userid = userinfo.Id;
				List<Medicine> medicines = _db.Medicines.Where(x => x.VetId == userid).OrderByDescending(o => o.Id).ToList();
				
				return View(medicines);
				}
			
			}
			return RedirectToAction("Logoff","Account");
		}
		
		
		[HttpGet]
		public ActionResult Addmed()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Addmed(Medicine medicines)
		{
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var OwnerId = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
				int VetsId = OwnerId.Id;
				medicines.VetId = VetsId;
				
			  	TempData["medmsg"] ="text";
			  	
				_db.Medicines.Add(medicines);
				_db.SaveChanges();
				
				return View();
			}
			
			
			return RedirectToAction("Logoff", "Account");
		}
		
		[Authorize]
		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var med = _db.Medicines.Find(Id);
			if(med != null)
			{
				ViewBag.Id = Id;
				return View(med);							
			}
			return RedirectToAction("Index");
		
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult Edit(Medicine med)
		{
			
			var medicine = _db.Medicines.Find(med.Id);
			
			medicine.Name = med.Name;
			medicine.Brand = med.Brand;
			medicine.Price = med.Price;
			
			_db.Entry(medicine).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			
			return RedirectToAction("Index");
		
		}
		
		[Authorize]
		public ActionResult Delete(int Id)
		{
			var med = _db.Medicines.Find(Id);
			
			if(med != null)
			{
				_db.Medicines.Remove(med);
				_db.SaveChanges();		
			
			} 
			return RedirectToAction("Index");
		
		
		}
	}
}