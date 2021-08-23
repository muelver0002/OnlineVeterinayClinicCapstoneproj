using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	
	public class DiagnoseController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		[Authorize]
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner"))
				{
					var user1 = Session["user"].ToString();
					var owner = _db.Vetowners.Where(x => x.Username == user1).FirstOrDefault();
					
					int OwnerId = owner.Id;
					
					List<Diagnose> ownerdiagnose = _db.Diagnoses.Where(x => x.Vetid == OwnerId).OrderByDescending(o => o.Id).ToList();
				
					return View(ownerdiagnose);
							
				}
					
					
				var user = Session["user"].ToString();
				var DocUser = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
				
				int VetId = DocUser.Vetid;
				
				List<Diagnose> Docdiagnose = _db.Diagnoses.Where(x => x.Vetid == VetId).OrderByDescending(o => o.Id).ToList();
				
				return View(Docdiagnose);
			
			}
			
			
			return RedirectToAction("Logoff", "Account");
		}
		
		
		[Authorize]
		public ActionResult Add(int? Id, string key)
		{
			
		if(Session["user"] !=null)
		  {
			ViewBag.Datestart = DateTime.Today;
			
			var user = Session["user"].ToString();
			var vetid = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
			int vetId = vetid.Vetid;
			ViewBag.Id = Id;
			Patient patient = _db.Patients.Find(Id);
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
			return RedirectToAction("index","reception");
			}
		  return RedirectToAction("Logoff", "Account");
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult Add(Diagnose diagnose,Diagnoserecord history , Returncheckup returncheck,Admission admission, int ID, string Followupdate, string DateStart, string DateEnd)
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
				var patient = _db.Patients.Find(ID);
				
				if(patient != null)
				{
					_db.Patients.Remove(patient);
					_db.SaveChanges();
					
				}
				
			   return RedirectToAction("index", "Diagnose");
			
			}
			return RedirectToAction("Logoff", "Account");
		}
		
//		public ActionResult Search(string key, int Id)
//		{
//			if(Session["user"] != null)
//			{
//				if(User.IsInRole("doctor"))
//				{
//					var user = Session["user"].ToString();
//					var Userinfo = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
//					int uservetid = Userinfo.Vetid;
//					if(!string.IsNullOrEmpty(key))
//					{
//					 List<Diagnoserecord> history = _db.Diagnoserecords.Where(x => x.Vetid == uservetid && x.Customername.ToLower().Contains(key.ToLower())).OrderByDescending(o => o.Id).ToList();				
//					 ViewBag.history = history;
//					 return View();
////					 return RedirectToAction("add","diagnose", Id);
//					}
//				}
//				return View();
////				return RedirectToAction("add","diagnose", Id);
//			}
//			return RedirectToAction("Logoff", "Account");
//		
//		}
		
		public ActionResult Delete(int Id)
		{
			var diagnose = _db.Diagnoses.Find(Id);
			if(diagnose != null)
			{
				_db.Diagnoses.Remove(diagnose);
				_db.SaveChanges();
			
			}
		     return RedirectToAction("Index");
		
		}
		
	}
}