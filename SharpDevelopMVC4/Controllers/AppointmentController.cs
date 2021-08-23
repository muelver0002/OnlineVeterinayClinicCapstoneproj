using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	
	public class AppointmentController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		[Authorize]
		public ActionResult Index()
		{
			string status = "Pending";
     		if(Session["user"] != null)
			{
     			if(User.IsInRole("owner"))
     			{
     				var user = Session["user"].ToString();
     				var vetuser = _db.Vetowners.Where(x => x.Username == user ).FirstOrDefault();     			   
     				int VetUserId = vetuser.Id;
     				
     				List<Appointment> Adminappointment = _db.Appointments.Where(x => x.VetId == VetUserId && x.Status.ToLower().Contains(status.ToLower())).OrderByDescending(o => o.Id).ToList();
     			    
     			    return View(Adminappointment);
     			
     			}
     			
     			
     		string Status ="Pending";
			var username = Session["user"].ToString();
			var Vet = _db.Receptionists.Where(x => x.Username == username).FirstOrDefault();		
			int userId = Vet.VetId;
			
			List<Appointment> appointment = _db.Appointments.Where(x => x.VetId == userId && x.Status.ToLower().Contains(Status.ToLower())).OrderByDescending(o => o.Id).ToList();
//			int i = 0;
//			foreach(var model in appointment)
//			{
//				int sample = model.Id;
//				i += 1;
//			    
//				
//			}
//			
//			TempData["notify"] = i;
			return View(appointment);   
			}
			
			return RedirectToAction("Logoff", "Account");
			
			
//			var list = _db.Appointments.ToList();		
//			return View(list);
		}
		
		
		//View Appointment for customer
		[Authorize]
		public ActionResult Appointmnet()
		{
			if(Session["user"] != null)
			{
				
			if(User.IsInRole("customer"))
     			{
     				ViewBag.Status="Approved";
     				
     				var usercus = Session["user"].ToString();
     				var custId = _db.Customers.Where(x => x.Username == usercus ).FirstOrDefault();     				
     				int CustId = custId.Id;
     			
     				List<Appointment> forcustomer = _db.Appointments.Where(x => x.CustId == CustId).OrderByDescending(o => o.Id).ToList();
     				
     				return View(forcustomer);
     			
     			}
			    TempData["deletemsg"] = null;
			}
			
			return RedirectToAction("Logoff","Account");
		
		
		}
		
		[Authorize]
		public ActionResult Add(int? ID= null)
		{
			if(User.IsInRole("customer"))
            {		
				if(Session["user"] != null)
				{
					
					
					Product vetname = _db.Products.Find(ID);
					ViewBag.Name = vetname.Name;
					ViewBag.Id = ID;
					
					var username = Session["user"].ToString();
					var cus = _db.Customers.Where(x => x.Username == username).FirstOrDefault();
					
					int cusId = cus.Id;
									
					List<Servicesacon> Serviceid = _db.Servicesacons.Where(x => x.VetId == ID).ToList();
					ViewBag.services = Serviceid;
					
					
					List<Pet> petId = _db.Pets.Where(x => x.OwnersID == cusId).ToList();
					ViewBag.pets = petId;
										
					Customer customer = _db.Customers.Find(cusId);
					
					return View(customer);
			
				}
			}
			
			return View();
		
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult Add(Appointment app, int Pet, string[] Services = null)
		{   
			int Id = app.VetId;
			string serve="";
			Product product = _db.Products.Find(Id);
			if(Services != null)
			{			
				foreach(var service in Services)
				{
				   
					serve += service +", ";
				
				}
			}
			if(Services == null)
			{
			
			  TempData["noservice"] =" Please select services...";
			  return RedirectToAction("add","appointment", product);
			}
			var Petinfo = _db.Pets.Where(x => x.Id == Pet).FirstOrDefault();
			
			string pettype = Petinfo.Type;
			var animals = _db.Animalsacoms.Where(x => x.VetId == Id && x.Name.ToLower().Contains(pettype.ToLower())).FirstOrDefault();
			if(animals == null)
			{   
				TempData["notaval"] = pettype +" is not available....";
				return RedirectToAction("add","appointment", product);
			}
			app.PetName = Petinfo.PetName;
			app.Color = Petinfo.Color;
			app.Type = Petinfo.Type;
			app.Breed = Petinfo.Breed;
			app.Bloodtype = Petinfo.Bloodtype;
			app.Gender = Petinfo.Gender;
			app.Bdate = Petinfo.Bdate;
			app.Status = "Pending";
			app.Concern = serve;
			
			TempData["notification"] ="text";
			
			TempData["customer"] = "Appointment sent successfuly.";
			_db.Appointments.Add(app);
			_db.SaveChanges();
			
			
			
			return RedirectToAction("details","crudsample", product);
		
		}
		
		[Authorize]
		public ActionResult Cancel(int Id)
		{
			var app = _db.Appointments.Find(Id);
			if(app != null)
			{
				_db.Appointments.Remove(app);
				_db.SaveChanges();	
				TempData["deletemsg"]="Canceled Successfuly .";
			}
			return RedirectToAction("Appointmnet");
		
		}
		
		[Authorize]
		public ActionResult Delete(int Id)
		{
			var app = _db.Appointments.Find(Id);
			if(app != null)
			{
				_db.Appointments.Remove(app);
				_db.SaveChanges();	
				TempData["deletemsg"]="Deleted Successfuly .";
			}
			return RedirectToAction("Appointmnet");
		
		}
	}
}