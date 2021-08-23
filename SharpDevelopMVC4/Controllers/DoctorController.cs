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
	/// Description of DoctorController.
	/// </summary>
	public class DoctorController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner")){
				var user = Session["user"].ToString();
				var vetadmin = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
				int VetId = vetadmin.Id;
				
				List<Doctor> DoctorList = _db.Doctors.Where(x => x.Vetid == VetId).ToList();
				
				return View(DoctorList);
				}
				
				
			}
			
			
              return RedirectToAction("Logoff", "Account");
		}
		
		
		
		
		public ActionResult Register()
		{
			
			return View();
		}
		
		[HttpPost]
		public ActionResult Register(RegisterViewModel newUser, string RetypePassword )
		{
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var vetId = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				int Id = vetId.Id;
			
			if(newUser.Password == RetypePassword)
			{
			
				var Regis = UserAccount.Create(newUser.UserName, newUser.Password, "doctor");
				if(Regis != null)
				{
					var newDoc = new Doctor();
					
					newDoc.Fullname = newUser.Fullname;
					newDoc.Username = newUser.UserName;
					newDoc.Password = newUser.Password;
					newDoc.Vetid = Id;
					ViewBag.doc="text";
					_db.Doctors.Add(newDoc);
					_db.SaveChanges();
					
					
					return View();
				
				
				}
				
				ViewBag.message = "Registration Failed";
			
			
			}
			
			else{
				ViewBag.message="Password not matched";
			   }
			   return View();
			}
			
			
			return RedirectToAction("Logoff", "Account");
		}
	}
}