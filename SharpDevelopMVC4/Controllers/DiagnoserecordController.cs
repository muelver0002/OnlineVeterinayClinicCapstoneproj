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
	/// Description of DiagnoserecordController.
	/// </summary>
	public class DiagnoserecordController : Controller
	{   
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		[Authorize]
		public ActionResult Index(string Searchkey)
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner"))
				{
					if(string.IsNullOrEmpty(Searchkey))
					{
					var users = Session["user"].ToString();
					var owneruser = _db.Vetowners.Where(x => x.Username == users).FirstOrDefault();
					
					int VetId = owneruser.Id;
					
					List<Diagnoserecord> history = _db.Diagnoserecords.Where(x => x.Vetid == VetId).OrderByDescending(o => o.Id).ToList();				
					return View(history);
					}
					
					else
					{
					var users = Session["user"].ToString();
					var owneruser = _db.Vetowners.Where(x => x.Username == users).FirstOrDefault();
					
					int VetId = owneruser.Id;
					
					List<Diagnoserecord> history = _db.Diagnoserecords.Where(x => x.Vetid == VetId && x.Customername.ToLower().Contains(Searchkey.ToLower())).OrderByDescending(o => o.Id).ToList();
					return View(history);
					}
				}
				if(User.IsInRole("doctor"))
				{
					if(string.IsNullOrEmpty(Searchkey))
					{
			        var user = Session["user"].ToString();
					var Docuser = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
					
					int VetIddoc = Docuser.Vetid;
					List<Diagnoserecord> historydoc = _db.Diagnoserecords.Where(x => x.Vetid == VetIddoc).OrderByDescending(o => o.Id).ToList();				
					return View(historydoc);
					}
					
					else
					{
					 var user = Session["user"].ToString();
					var Docuser = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
					
					int VetIddoc = Docuser.Vetid;
					List<Diagnoserecord> historydoc = _db.Diagnoserecords.Where(x => x.Vetid == VetIddoc && x.Customername.ToLower().Contains(Searchkey.ToLower())).OrderByDescending(o => o.Id).ToList();
					return View(historydoc);
					
					}
				}
			}
			
			
			return RedirectToAction("Logoff", "Account");
		}
		
		
		public ActionResult Add()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		public ActionResult Add(Diagnoserecord history)
		{
			return View();
		}
		
		
		[Authorize]
		public ActionResult Viewhistory()
		{
			if(Session["user"] != null)
			{
			if(User.IsInRole("customer"))
				{
					var cususer = Session["user"].ToString();
					var CustomId = _db.Customers.Where(x => x.Username == cususer).FirstOrDefault();
					
					int customerId = CustomId.Id;
				
					List<Diagnoserecord> custhistory = _db.Diagnoserecords.Where(x => x.CustId == customerId).OrderByDescending(o => o.Id).ToList();
					return View(custhistory);
				}
			
			    return RedirectToAction("Logoff","Account");
			}
			
			
			return RedirectToAction("Logoff","Account");
		}
		
		[Authorize]
		public ActionResult ReturnCheckUp()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("customer"))
				{
					var user = Session["user"].ToString();
					var userIfo = _db.Customers.Where(x => x.Username == user).FirstOrDefault();
					
					int userId = userIfo.Id;
					
					List<Diagnoserecord> returncheck = _db.Diagnoserecords.Where(x => x.CustId == userId && x.Followupdate != null).OrderByDescending(o => o.Id).ToList();
				 
					return View(returncheck);
				}
				
				return RedirectToAction("Logoff","Account");
					
			}
			
              	return RedirectToAction("Logoff","Account");		
		
		
		}
		
	}
}