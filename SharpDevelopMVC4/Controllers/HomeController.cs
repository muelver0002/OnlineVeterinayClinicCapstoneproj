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
    public class HomeController : Controller
    { 
    	private static SdMvc4DbContext _db = new SdMvc4DbContext();
    	
        public ActionResult Index()
        {
   	   	    if(Session["user"] != null)
            {
            	if(User.IsInRole("owner"))
            	{   
            		
            		
            		
            		var user = Session["user"].ToString();
            		var userinfo = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
            		
            		int ownerId = userinfo.Id;
            			
            		var userexist = _db.Payments.Where(x => x.VetId == ownerId).FirstOrDefault();
            		if(userexist == null){
						
						TempData["pays"] = "msg";
						return RedirectToAction("Index","Payment");					
					}
            		var due = userexist.Duedate;
            		int payId = userexist.Id;
            		string name = userinfo.Fullname;
            		if(DateTime.Now.Date >= due)
            		{
            			var payment = _db.Payments.Find(payId);
            			if(payment != null)
            			{
            				_db.Payments.Remove(payment);
            				_db.SaveChanges();
            				TempData["duemsg"]= name +",Your Subscription has Expired. Please pay your billing for approval Thank You..";
            			    return RedirectToAction("Create","Payment");
            			}
            		
            		}
            		if(userexist != null)            		
            		{
            		
            	    string mdbs = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.GetData("DataDirectory") + @"\MyAccessDb.mdb";

		            using (var conn = new OleDbConnection(mdbs))
		            {
		                // conn.Execute( "INSERT INTO contacts(FullName, Email, BirthDate) "
		                // + "VALUES (@FullName, @Email, @BirthDAte)", contact);
		                var contactList = conn.Query("Select Id, FullName, Email, BirthDate from contacts").ToList();
		                ViewBag.Data = JsonConvert.SerializeObject(contactList);
		            }

           
            	
          			  return View();
            		
            		}
            		
            	
            	}
            
            }
        	
        	string mdb = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.GetData("DataDirectory") + @"\MyAccessDb.mdb";

            using (var conn = new OleDbConnection(mdb))
            {
                // conn.Execute( "INSERT INTO contacts(FullName, Email, BirthDate) "
                // + "VALUES (@FullName, @Email, @BirthDAte)", contact);
                var contactList = conn.Query("Select Id, FullName, Email, BirthDate from contacts").ToList();
                ViewBag.Data = JsonConvert.SerializeObject(contactList);
            }

           
            	
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [Authorize]
        public ActionResult ForAuthUser()
        {
        	ViewBag.Message = "Authorized user page. ";

            return View("About");
        }
        
        [Authorize(Roles="admin")]
        public ActionResult Forgotpass()
        {
        	return View();
        
        }
        
        
        [Authorize(Roles="admin")]
        [HttpPost]
        public ActionResult Forgotpass(string username, string oldpass="", string newpass="")
        {
            ViewBag.Message = "Authorized ADMIN page.";
            
            //var oldpass = string.Empty;
            var user = _db.Users.Where(x => x.UserName == username.Trim()).FirstOrDefault();
            if(user != null)
            {
            bool result = UserAccount.ChangePassword(username, oldpass, newpass, true);
            if(result)
            {
            
                   TempData["mgsAlert"] ="Password successfuly changed.";
				   return View("Index");
            
            }
            }
            TempData["mgsAlertfailed"] ="username not exist.";
		    return View();
        } 




        [Authorize]
        public ActionResult Dashboard()
        {
           
        	if(Session["user"] != null)
			{
     			
     		if(User.IsInRole("recept"))
     		{
     	    string Status ="Pending";
			var username = Session["user"].ToString();
			var Vet = _db.Receptionists.Where(x => x.Username == username).FirstOrDefault();		
			int userId = Vet.VetId;
			
			List<Appointment> appointment = _db.Appointments.Where(x => x.VetId == userId && x.Status.ToLower().Contains(Status.ToLower())).OrderByDescending(o => o.Id).ToList();
			int i = 0;
			foreach(var model in appointment)
			{
				int sample = model.Id;
				i += 1;		
			}
			
			if(i != 0)
			 {
				TempData["notify"] = (i);
		  	 }
//			else
//			 {
//				TempData["notify"] = (0);
//			
//			 }
			return View();
     		}
     		
     		
     		return RedirectToAction("Logoff","Account");
			}
        	
        	return View();
        }
        
                
        
        
        public ActionResult Autosearch()
        {
        	
        	
        	return View();
        
        
        }
        
        
        [Authorize(Roles="customer")]
        public ActionResult Menu()
        {
        if(Session["user"] != null)
			{
				if(User.IsInRole("customer"))
				{
					
					//for appointment
					var user1 = Session["user"].ToString();
					var userinfo1 = _db.Customers.Where(x => x.Username == user1 ).FirstOrDefault();
					
					int userId1 = userinfo1.Id;
					string Status ="Pending";
					List<Appointment> appointment = _db.Appointments.Where(x => x.CustId == userId1 && x.Status.ToLower().Contains(Status.ToLower())).ToList();
					int num = 0;
					foreach(var model in appointment)
					{
						string sample = model.Status;
						num += 1;		
					}
					
					if(num != 0)
					 {
						TempData["appoint"] = num;
				  	 }

		            // return checkup
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
						TempData["return"] = i;
				  	 }
					
                    //for admssion
					var user2 = Session["user"].ToString();
					var userinfo2 = _db.Customers.Where(x => x.Username == user2).FirstOrDefault();
					int customerId = userinfo2.Id;
					
					List<Admission> admission = _db.Admissions.Where(x => x.CustId == customerId).ToList();				
					int y = 0;
					foreach(var model in admission)
					{
						int sample = model.Id;
						y += 1;
					
					}
					if(y != 0)
					{
						TempData["admissioncus"] = y;
					}
					
					
					return View();
				
				}
			return View();
			    
			}
        	return View();
        
        }
        
        [Authorize(Roles="doctor")]
        public ActionResult Docmenu()
        {
        	if(Session["user"] != null)
        	{
        		var user = Session["user"].ToString();
        		var userInfo = _db.Doctors.Where(x => x.Username == user).FirstOrDefault();
        		int docvetid = userInfo.Vetid;
          		List<Admission> admissions = _db.Admissions.Where(x => x.Vetid == docvetid).ToList();
        		
        		    int i = 0;
					foreach(var model in admissions)
					{
						int sample = model.Id;
						i += 1;		
					}
					
					if(i != 0)
					 {
						TempData["admit"] = i;
				  	 }
        	
					var user1 = Session["user"].ToString();
					var userInfo1 = _db.Doctors.Where(x => x.Username == user1).FirstOrDefault();
					int docvetid1  = userInfo1.Vetid;
				    List<Patient> patient = _db.Patients.Where(x => x.Vetid == docvetid1).ToList();
					
				    int num = 0;
					foreach(var model in patient)
					{
						int sample = model.Id;
						num += 1;		
					}
					
					if(num != 0)
					 {
						TempData["patient"] = num;
				  	 }
					
					
					var user2 = Session["user"].ToString();
					var userdoc = _db.Doctors.Where(x => x.Username == user2).FirstOrDefault();
					int docvetid2 = userdoc.Vetid;
					
					List<Returncheckup> returncheck = _db.Returncheckups.Where(x => x.Vetid == docvetid2).ToList();
						
                    int num1 = 0;
					foreach(var model in returncheck)
					{
						int sample = model.Id;
						num1 += 1;		
					}
					
					if(num1 != 0)
					 {
						TempData["returns"] = num1;
				  	 }
					
					return View();
        	}
        	
        	
        	return View();
        
        }
        [Authorize]
        public ActionResult Sample()
        {
        if(Session["user"] != null)
			{
     			
     		if(User.IsInRole("recept"))
     		{
     	    string Status ="Pending";
			var username = Session["user"].ToString();
			var Vet = _db.Receptionists.Where(x => x.Username == username).FirstOrDefault();		
			int userId = Vet.VetId;
			
			List<Appointment> appointment = _db.Appointments.Where(x => x.VetId == userId && x.Status.ToLower().Contains(Status.ToLower())).OrderByDescending(o => o.Id).ToList();
			int i = 0;
			foreach(var model in appointment)
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
			return View();
     		}
     		
     		
     		return RedirectToAction("Logoff","Account");
			}
        	return View();
        
        }
                
    }
}