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
	/// Description of ReturncheckupController.
	/// </summary>
	public class ReturncheckupController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		public ActionResult Index()
		{
			
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var docuser = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
				
				int docid = docuser.Vetid;
				
				List<Returncheckup> returncheck = _db.Returncheckups.Where(x => x.Vetid == docid).ToList();
			
				return View(returncheck);
			
			}
			
			return RedirectToAction("Logoff","Account");
		}
		
		
		[HttpGet]
		public ActionResult Add(int? Id, string key)
		{
		
			if(Session["user"] !=null)
		  {
			
			var user = Session["user"].ToString();
			var vetid = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
			int vetId = vetid.Vetid;
			ViewBag.Id = Id;
			Returncheckup patient = _db.Returncheckups.Find(Id);
			if(patient != null)
			{
			if(string.IsNullOrEmpty(key))
				{
			        // history per patient 
			        int patientId = patient.CustId;
					List<Diagnoserecord> diagnose = _db.Diagnoserecords.Where(x => x.CustId == patientId && x.Vetid == vetId && x.CustId != 0).ToList();
					ViewBag.history = diagnose;
					return View(patient);
				}
				if(!string.IsNullOrEmpty(key))
			    {
			    	
			     List<Diagnoserecord> diagnose = _db.Diagnoserecords.Where(x => x.Vetid == vetId && x.Customername.ToLower().Contains(key.ToLower())).ToList();
				 ViewBag.history = diagnose;
				 return View(patient);
			    }
			}
			return RedirectToAction("index","Returncheckup");
			}
		  return RedirectToAction("Logoff", "Account");
		
		}
		
		[HttpPost]
		public ActionResult Add(Diagnose diagnose,Diagnoserecord history, Returncheckup returncheck , Admission admission, int ID, string Followupdate,string DateStart, string DateEnd)
		{
		
		   if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var doctor = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
				
				int DocId = doctor.Vetid;
				string Docname = doctor.Fullname;
				
				
				diagnose.DocName = Docname;
				diagnose.Vetid = DocId;
				diagnose.Datetoday = DateTime.Now;
				
				TempData["diagnosemsg"]="text";
				TempData["msg"] = "hey";
				
				_db.Diagnoses.Add(diagnose);
				_db.SaveChanges();
			
				//Diagnose History//
				history.DocName = Docname;
				history.Vetid = DocId;
				history.Datetoday = DateTime.Now;
				
				_db.Diagnoserecords.Add(history);
				_db.SaveChanges();
				
					//admission 
				if(!string.IsNullOrEmpty(DateStart) && !string.IsNullOrEmpty(DateEnd))
				{
					admission.DocName = Docname;
					admission.Vetid = DocId;
					admission.Datetoday=DateTime.Now;
					_db.Admissions.Add(admission);
					_db.SaveChanges();
								
				}
				
				
				//returncheckup
				if(!string.IsNullOrEmpty(Followupdate))
				{
					returncheck.DocName = Docname;
					returncheck.Vetid = DocId;
					returncheck.Datetoday = DateTime.Now;
				    
					_db.Returncheckups.Add(returncheck);
					_db.SaveChanges();
				
				}
				
				var patient = _db.Returncheckups.Find(ID);
				
				if(patient != null)
				{
					_db.Returncheckups.Remove(patient);
					_db.SaveChanges();
					
				}
				
			   return RedirectToAction("index", "Diagnose");
			
			}
			return RedirectToAction("Logoff", "Account");
		
		
		}
		
		
		
		public ActionResult Delete(int Id)
		{
			var p = _db.Returncheckups.Find(Id);
			
			if(p !=null)
			{
				_db.Returncheckups.Remove(p);
				_db.SaveChanges();
				TempData["deletpet"] ="deletpet";
			}
			
			return RedirectToAction("Index");
		}
		
		
		
		[Authorize(Roles="customer")]
		public ActionResult Returncheck()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("customer"))
				{
					var user = Session["user"].ToString();
					var userinfo = _db.Customers.Where(x => x.Username == user ).FirstOrDefault();
					
					int userId = userinfo.Id;
					
					List<Returncheckup> returncheck = _db.Returncheckups.Where(x => x.CustId == userId).OrderByDescending(o => o.Id).ToList();
				
					
					int i = 0;
					foreach(var model in returncheck)
					{
						int sample = model.Id;
						i += 1;		
					}
					
					if(i != 0)
					 {
						TempData["notify"] = (i);
				  	 }
					else
					 {
						TempData["notify"] = (0);
					
					 }
					return View(returncheck);
				    
					
				
				}
			
			    
			}
		    
			return RedirectToAction("Logoff","Account");
		
		}
	}
}