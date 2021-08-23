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
	/// Description of ReceptController.
	/// </summary>
	public class ReceptController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		[Authorize]
		public ActionResult Sales(string Searchkey)
		{   
			if(Session["user"] != null)
			{
				ViewBag.cusname =Searchkey;
				
				var user = Session["user"].ToString();
				var ReceptInfo = _db.Receptionists.Where(x => x.Username == user).FirstOrDefault();
				
				int VetiId = ReceptInfo.VetId;
				int RepId = ReceptInfo.Id;
				
			    //ServiceDropdown
				List<Servicesacon> servicelist = _db.Servicesacons.Where(x => x.VetId == VetiId).ToList();
				ViewBag.Servicelist = servicelist;
				
				//ServiceDropdown
				List<Medicine> medicines = _db.Medicines.Where(x => x.VetId == VetiId).ToList();
				ViewBag.MedList = medicines;
			
			
				List<Productacom> product = _db.Productacoms.Where(x => x.VetId == VetiId).ToList();
				ViewBag.prod = product;	
				
				
				
					
			ViewBag.empty = Searchkey;
			if(string.IsNullOrEmpty(Searchkey))
			{

			int num = 0;
			int total = 0;
			foreach(var Item in _db.Recepts.Where(x => x.ReceptionistId == RepId  && x.VetId == VetiId && x.Date == DateTime.Today).ToList())
			{				
				num += Item.Total;		
			}
			total = num;		
			ViewBag.TotalCount = total;	
			
			List<Recept> reps = _db.Recepts.Where(x => x.ReceptionistId == RepId && x.VetId == VetiId && x.Date == DateTime.Today).ToList();
			return View(reps);
			
			}
			
			else
			{
			  
			int num = 0;
			int total = 0;
			foreach(var Item in _db.Recepts.Where(x => x.CustName.ToLower().Contains(Searchkey.ToLower()) && x.VetId == VetiId && x.Date == DateTime.Today).ToList())
			{				
				num += Item.Total;		
			}
			total = num;		
			ViewBag.TotalCount = total;			
			
			List<Recept> rep = _db.Recepts.Where(x => x.CustName.ToLower().Contains(Searchkey.ToLower()) && x.VetId == VetiId && x.Date == DateTime.Today).ToList();
			return View(rep);
				
			}
			
			}
			return View();
			
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult Sales (Recept recepts, Salesrecord salesrecord, int medicineId,int productId,int serviceId, int qty1=0, int qty2=0)
		{
			var medicines = _db.Medicines.Find(medicineId);
			var services = _db.Servicesacons.Find(serviceId);
			var products = _db.Productacoms.Find(productId);
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var userinfo = _db.Receptionists.Where(x => x.Username == user).FirstOrDefault();
				int VetID = userinfo.VetId;
				int ReceptionId = userinfo.Id;
			
			 if(services != null)
		      {
				int serviceprice = services.Price;		
				
				recepts.PName = services.Servicesname;				
				recepts.VetId = VetID;				
				recepts.Date = DateTime.Today;				
				recepts.ReceptionistId = ReceptionId;				
				recepts.Price = serviceprice;
				recepts.Total = serviceprice;
				
				_db.Recepts.Add(recepts);
				_db.SaveChanges();
				
				
				salesrecord.PName = services.Servicesname;
				salesrecord.VetId = VetID;
				salesrecord.Date = DateTime.Today;
				salesrecord.ReceptionistId = ReceptionId;
				salesrecord.Price = serviceprice;
				salesrecord.Total = serviceprice;
				
				
				_db.Salesrecords.Add(salesrecord);
				_db.SaveChanges();
				
			  }

			if(products != null)
			{
				int proprice = products.Price;
				int prototal = 0;
				
				prototal = proprice * qty2;
				
				recepts.PName = products.Productname;
				recepts.ReceptionistId = ReceptionId;
				recepts.VetId = VetID;
				recepts.Date = DateTime.Today;
				if(qty2 == 0)
				{
				recepts.Price = proprice;
				recepts.Total = proprice;
				recepts.Qty = qty2;
				}
				else{
				recepts.Price = proprice;
				recepts.Total = prototal;
				recepts.Qty = qty2;
				}
				_db.Recepts.Add(recepts);
				_db.SaveChanges();
				
				
				salesrecord.PName = products.Productname;
				salesrecord.ReceptionistId = ReceptionId;
				salesrecord.VetId = VetID;
				salesrecord.Date = DateTime.Today;
				if(qty2 == 0){
				salesrecord.Price = proprice;
				salesrecord.Total = proprice;
				salesrecord.Qty = qty2;
				}
				else{
				salesrecord.Price = proprice;
				salesrecord.Total = prototal;
				salesrecord.Qty = qty2;
				}
				_db.Salesrecords.Add(salesrecord);
				_db.SaveChanges();
			}
		   
			if(medicines != null)
			{
				int medprice = medicines.Price;
				int medtotal =0;
				
				medtotal = medprice * qty1;				
				recepts.PName = medicines.Name;				
				recepts.ReceptionistId = ReceptionId;				
				recepts.VetId = VetID;				
				recepts.Date = DateTime.Today;	
				if(qty1 == 0)
				{
				recepts.Price = medprice;
				recepts.Total = medprice;
				recepts.Qty = qty1;
				}
				else{
				recepts.Price = medprice;
				recepts.Total = medtotal;
				recepts.Qty = qty1;
				}
				_db.Recepts.Add(recepts);
				_db.SaveChanges();
				
				salesrecord.PName = medicines.Name;				
				salesrecord.ReceptionistId = ReceptionId;				
				salesrecord.VetId = VetID;				
				salesrecord.Date = DateTime.Today;	

				if(qty1 == 0)
				{
				salesrecord.Price = medprice;
				salesrecord.Total = medprice;
				salesrecord.Qty = qty1;
				}
				else{
				salesrecord.Price = medprice;
				salesrecord.Total = medtotal;
				salesrecord.Qty = qty1;
				}
				_db.Salesrecords.Add(salesrecord);
				_db.SaveChanges();
			}
			
		
			}
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var ReceptInfo = _db.Receptionists.Where(x => x.Username == user).FirstOrDefault();
				
				int VetiId = ReceptInfo.VetId;
				int ReceptId = ReceptInfo.Id;
				
			    //ServiceDropdown
				List<Servicesacon> servicelist = _db.Servicesacons.Where(x => x.VetId == VetiId).ToList();
				ViewBag.Servicelist = servicelist;
				
				//ServiceDropdown
				List<Medicine> medicine = _db.Medicines.Where(x => x.VetId == VetiId).ToList();
				ViewBag.MedList = medicine;
			
			
				List<Productacom> product = _db.Productacoms.Where(x => x.VetId == VetiId).ToList();
				ViewBag.prod = product;	
				
				
				int num = 0;
				int total = 0;
				foreach(var Item in _db.Recepts.Where(x => x.ReceptionistId == ReceptId  && x.VetId == VetiId && x.Date == DateTime.Today).ToList())
				{				
					num += Item.Total;		
				}
				total = num;		
				ViewBag.TotalCount = total;	
				
				List<Recept> recpts = _db.Recepts.Where(x => x.ReceptionistId == ReceptId && x.VetId == VetiId && x.Date == DateTime.Today).ToList();
			    ViewBag.receptlist = recpts;
				
			    return View(recpts);
			}
			
			return View();
		
		
		}
		
		
		[Authorize]
		public ActionResult Delete(int Id)
		{
			var sale = _db.Recepts.Find(Id);
			
			if(sale != null)
			{
				_db.Recepts.Remove(sale);
				_db.SaveChanges();
			
			}
			var record = _db.Salesrecords.Find(Id);
			if(record != null)
			{
			    _db.Salesrecords.Remove(record);
				_db.SaveChanges();
			
			}
		    return RedirectToAction("Sales");
		}
		
		[Authorize]
		public ActionResult Pay(int total, int change, string Cusname)
		{
			
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var ReceptInfo = _db.Receptionists.Where(x => x.Username == user).FirstOrDefault();
				
				int VetiId = ReceptInfo.VetId;
				int ReceptId = ReceptInfo.Id;
				
			    //ServiceDropdown
				List<Servicesacon> servicelist = _db.Servicesacons.Where(x => x.VetId == VetiId).ToList();
				ViewBag.Servicelist = servicelist;
				
				//ServiceDropdown
				List<Medicine> medicine = _db.Medicines.Where(x => x.VetId == VetiId).ToList();
				ViewBag.MedList = medicine;
			
			
				List<Productacom> product = _db.Productacoms.Where(x => x.VetId == VetiId).ToList();
				ViewBag.prod = product;	
				
				
				int num = 0;
				int totalpay = 0;
				foreach(var Item in _db.Recepts.Where(x => x.ReceptionistId == ReceptId  && x.VetId == VetiId && x.Date == DateTime.Today).ToList())
				{				
					num += Item.Total;		
				}
				totalpay = num;		
				ViewBag.TotalCount = totalpay;	
				
				List<Recept> recpt = _db.Recepts.Where(x => x.ReceptionistId == ReceptId && x.VetId == VetiId && x.Date == DateTime.Today).ToList();
			    ViewBag.receptlist = recpt;
				
			    int num1 = 0;
				int num2 = 0;
				num1 = total;			
				num2 = num1 - change;		
				ViewBag.change = num2;
			    
				if(Cusname != null)
				{
					List<Recept> list = _db.Recepts.Where(x => x.CustName.ToLower().Contains(Cusname.ToLower())).ToList();
				
				foreach(var List in list)
				{
					int Id = List.Id;
					var g = _db.Recepts.Find(Id);
					
					_db.Recepts.Remove(g);
					_db.SaveChanges();
			
				}
				}
				
				
			    return View("Sales",recpt);
			}
		 
			
			return View();			
		}
		
		[Authorize]
		[HttpGet]
		public ActionResult AddItem(int? Id= null)
		{
			if(Id != null)
			{
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var Docinfo = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
				int DocId = Docinfo.Vetid;
				
				//ServiceDropdown
				List<Servicesacon> servicelist = _db.Servicesacons.Where(x => x.VetId == DocId).ToList();
				ViewBag.Servicelist = servicelist;
				
				//ServiceDropdown
				List<Medicine> medicines = _db.Medicines.Where(x => x.VetId == DocId).ToList();
				ViewBag.MedList = medicines;
				if( medicines == null)
				{
				 return RedirectToAction("Index","Diagnose");
				
				}
			
				List<Productacom> product = _db.Productacoms.Where(x => x.VetId == DocId).ToList();
				ViewBag.prod = product;
				
				List<Recept> recept = _db.Recepts.ToList();
				ViewBag.recpt = recept;
			}
			
				Diagnose diagnose = _db.Diagnoses.Find(Id);
				ViewBag.DiagnoseId = diagnose;
				if(diagnose == null)
				{
		          return RedirectToAction("Index","Diagnose");
				}
				return View();
			}
			
			return RedirectToAction("Index","Diagnose");
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult AddItem( Recept recepts,Salesrecord salesrocord, int serviceId, int medicineId, int Id, int PId, string cusname, string addmision, int qty1 =0,int days=0, int rate=0)
		{
			var medicines = _db.Medicines.Find(medicineId);
			var services = _db.Servicesacons.Find(serviceId);
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var userinfo = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
				int VetID = userinfo.Vetid;
				int DocId = userinfo.Id;
		  
				
			if(services != null)
			{
				int serviceprice = services.Price;
			
				recepts.DoctorId = DocId;
				
				recepts.PName = services.Servicesname;
				recepts.CustName = cusname;
				
				recepts.VetId = VetID;
				
				recepts.Date = DateTime.Today;
				recepts.CusId = Id;
				
				recepts.Price = serviceprice;
				recepts.Total = serviceprice;
			
			    
				_db.Recepts.Add(recepts);
				_db.SaveChanges();
				
				
				salesrocord.DoctorId = DocId;
				
				salesrocord.PName = services.Servicesname;
				salesrocord.CustName = cusname;
				
				salesrocord.VetId = VetID;
				
				salesrocord.Date = DateTime.Today;
				salesrocord.CusId = Id;
				
				salesrocord.Price = serviceprice;
				salesrocord.Total = serviceprice;
				
				
				
				
				_db.Salesrecords.Add(salesrocord);
				_db.SaveChanges();
			}   
			
			
			if(rate != 0 && days !=0)
		     {
			  	int Rate = rate;
			  	int Subtotal;
			  	
			  	Subtotal = Rate * days;
			  	recepts.Qty = days;
			  	recepts.Price =rate;
			  	recepts.Total = Subtotal;
			  	
		    	recepts.DoctorId = DocId;
		    	recepts.PName = "Admission";
		    	recepts.CustName= cusname;
		    	recepts.VetId =VetID;
		    	
		    	recepts.Date = DateTime.Today;
		    	recepts.CusId = Id;
		    
		    	_db.Recepts.Add(recepts);
		    	_db.SaveChanges();
		    	
		    	
		    
			  	salesrocord.Qty = days;
			  	salesrocord.Price =rate;
			  	salesrocord.Total = Subtotal;
			  	
		    	salesrocord.DoctorId = DocId;
		    	salesrocord.PName =  "Admission";
		    	salesrocord.CustName= cusname;
		    	salesrocord.VetId =VetID;
		    	
		    	salesrocord.Date = DateTime.Today;
		    	salesrocord.CusId = Id;
		    
		    	_db.Salesrecords.Add(salesrocord);
		    	_db.SaveChanges();
		    }
			if(medicines != null)
			{
				int medprice = medicines.Price;
				int medtotal =0;
				
				medtotal = medprice * qty1;
				
				recepts.DoctorId = DocId;
				
				recepts.PName = medicines.Name;
				
				recepts.CustName = cusname;
				
				recepts.VetId = VetID;
				
				recepts.Date = DateTime.Today;
				
				recepts.CusId = Id;
				if(qty1==0)
				{
				 recepts.Price = medprice;	
				 recepts.Total = medprice;	
				 recepts.Qty = qty1;
				}
				else{
				recepts.Price = medprice;
				recepts.Total = medtotal;
				recepts.Qty = qty1;
				}
				_db.Recepts.Add(recepts);
				_db.SaveChanges();
				
				
				
				salesrocord.DoctorId = DocId;
				
				salesrocord.PName = medicines.Name;
				
				salesrocord.CustName = cusname;
				
				salesrocord.VetId = VetID;
				
				salesrocord.Date = DateTime.Today;
				
				salesrocord.CusId = Id;
				if(qty1 == 0)
				{
				salesrocord.Price = medprice;
				salesrocord.Total = medprice;
				salesrocord.Qty = qty1;
				}
				else{
				salesrocord.Price = medprice;
				salesrocord.Total = medtotal;
				salesrocord.Qty = qty1;
				}
				_db.Salesrecords.Add(salesrocord);
				_db.SaveChanges();
			}
			
		
			}
		
			//return views
			List<Recept> recept = _db.Recepts.ToList();
			ViewBag.recpt = recept;
			if(Session["user"] !=null)
			{
		     var user = Session["user"].ToString();
			 var docId = _db.Doctors.Where(x => x.Username ==  user).FirstOrDefault();
				
			 int DocId = docId.Vetid;
				
			List<Medicine> medicine = _db.Medicines.Where(x => x.VetId == DocId).ToList();
			ViewBag.MedList = medicine;
			if(medicine == null)
			{
			 return RedirectToAction("Index","Diagnose");
			
			}
		    List<Servicesacon> servicelist = _db.Servicesacons.Where(x => x.VetId == DocId).ToList();
			ViewBag.Servicelist = servicelist;
			
			List<Recept> recpts = _db.Recepts.Where(x => x.CustName == cusname && x.VetId == DocId && x.Date == DateTime.Today).ToList();
			ViewBag.receptlist = recpts;
			}
			Diagnose diagnose = _db.Diagnoses.Find(PId);
			ViewBag.DiagnoseId = diagnose;		
			if(diagnose == null)
			{
				 return RedirectToAction("Index","Diagnose");			
			}
			return View();
		}
		
		
		public ActionResult Done(int Id)
		{
			var diagnose = _db.Diagnoses.Find(Id);
			if(diagnose != null)
			{
				_db.Diagnoses.Remove(diagnose);
				_db.SaveChanges();
			
			}
			return RedirectToAction("Index","Diagnose");
		
		}
		
		
		
		public ActionResult Remove()
		{
			if(Session["user"] != null)
			{ 
				var user = Session["user"].ToString();
				var userinfo = _db.Receptionists.Where(x => x.Username == user).FirstOrDefault();
				
				int ReceptId = userinfo.Id;
				
				List<Recept> recept = _db.Recepts.Where(x => x.ReceptionistId == ReceptId).ToList();
				foreach(var recep in recept)
				{
					int Id = recep.Id;
					var r = _db.Recepts.Find(Id);
					
					_db.Recepts.Remove(r);
					_db.SaveChanges();

				}
				
				
				return RedirectToAction("sales","Recept");
			
			}
		  
			return RedirectToAction("Index","Home");
		}
		
		
		//Sales Record
		[Authorize]
		public ActionResult Viewsales(DateTime? Dates)
		{
			if(Session["user"] != null)
			{
				//Veterinary Admin
				if(User.IsInRole("owner"))
				{
					var user = Session["user"].ToString();
					var OwnerInfo = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				    
					int OwnerId = OwnerInfo.Id;
				
					int num = 0;
					int total = 0;
					foreach(var Item in _db.Salesrecords.Where(x => x.VetId == OwnerId  && x.Date.Value.Month == Dates.Value.Month && x.Date.Value.Year == Dates.Value.Year).ToList())
					{				
						num += Item.Total;		
					}
					total = num;		
					ViewBag.TotalCount = total;		
									
					List<Salesrecord> viewsales = _db.Salesrecords.Where(x => x.VetId == OwnerId && x.Date.Value.Month == Dates.Value.Month && x.Date.Value.Year == Dates.Value.Year).OrderByDescending(o => o.Id).ToList();
					return View(viewsales);
					
//					else
//					{
//						
//					List<Salesrecord> viewsale = _db.Salesrecords.Where(x => x.VetId == OwnerId).OrderByDescending(o => o.Id).ToList();
//					return View(viewsale);
//					
//					}
					
				}
				
				
				
				
				// System Admin
			   if(User.IsInRole("admin"))
			   {
 
		
			   	List<Salesrecord> saleslist = _db.Salesrecords.Where(x => x.Date.Value.Month ==  Dates.Value.Month && x.Date.Value.Year == Dates.Value.Year).ToList();
				return View(saleslist); 
				
			   }
				
			}
				
			return RedirectToAction("Logoff","Account");
		
		   
		}
			
	}
}