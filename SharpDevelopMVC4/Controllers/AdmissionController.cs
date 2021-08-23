using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using Dapper;
using Newtonsoft.Json;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	/// <summary>
	/// Description of AdmissionController.
	/// </summary>
	public class AdmissionController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		[Authorize]
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("doctor"))
				{
					var user = Session["user"].ToString();
					var userinfo = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();					
					int DocVetId = userinfo.Vetid;
					
					List<Admission> admissions = _db.Admissions.Where(x => x.Vetid == DocVetId).OrderByDescending(o => o.Id).ToList();
					return View(admissions);
				}
				
												
			  return RedirectToAction("Logoff","Account");
			}
			  return RedirectToAction("Logoff","Account");
		}
		
		//AdmissionList 4 customer..
		[Authorize(Roles="customer")]
		public ActionResult Customer()
		{
			if(Session["user"] != null)
			{
			if(User.IsInRole("customer"))
				{
					var user = Session["user"].ToString();
					var cusinfo = _db.Customers.Where(x => x.Username == user).FirstOrDefault();
					int CusId = cusinfo.Id;
					
					List<Admission> admissions = _db.Admissions.Where(x => x.CustId == CusId).ToList();					
					return View(admissions);				
				}
			return RedirectToAction("Logoff","Account");
			
			}
		    return RedirectToAction("Logoff","Account");
		}
		public ActionResult Details(int id)
		{
			return View();
		}
		
		public ActionResult Create()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Create(FormCollection values)
		{
			return View();
		}
		
		public ActionResult Edit(int id)
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Edit(int id, FormCollection values)
		{
			return View();
		}
		
		public ActionResult Delete(int Id)
		{
			var addmit = _db.Admissions.Find(Id);
			if(addmit != null)
			{
				_db.Admissions.Remove(addmit);
				_db.SaveChanges();
				
				TempData["admitmsg"] ="Deleted Successfuly";
			
			}
			  return RedirectToAction("Index");
			
		}
	}
}