using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	/// <summary>
	/// Description of AccountController.
	/// </summary>
	public class AccountController : Controller
	{
		
		SdMvc4DbContext _db = new SdMvc4DbContext();
		public ActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]		
		public ActionResult Login(string username, string password, bool rememberme = false)
		{
			 ViewBag.text = username;
			if(UserAccount.Authenticate(username, password))
		    {
				
		    	var user = UserAccount.GetUserByUserName(username);
				var authTicket = new FormsAuthenticationTicket(
				    1,                             	// version
				    user.UserName,               	// user name
				    DateTime.Now,                  	// created
				    DateTime.Now.AddMinutes(20),   	// expires
				    rememberme,                    	// persistent?
		    		user.Roles              		// can be used to store roles
			    );
				
				string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
				
				var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
				System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
				
				Session["user"] = user.UserName;
				
				return Redirect(FormsAuthentication.GetRedirectUrl(user.UserName, rememberme)); // auth succeed
				
		    }
		    
		    // invalid username or password
		    ModelState.AddModelError("", "Invalid username or password");
		    return View();
		}
		
		
		public ActionResult Logoff()
		{   
			Session.Clear();
		    FormsAuthentication.SignOut();
		    return RedirectToAction("login", "account");
		}
		
		
	    public ActionResult Register()
		{
		    List<Address> address = _db.Addreses.ToList();
		    ViewBag.Address = address;
			return View();
		}
		
		[HttpPost]
		public ActionResult Register(RegisterViewModel newUser, string RetypePassword)
		{
			
			if(newUser.Password == RetypePassword) {
				
		     var res = UserAccount.Create(newUser.UserName, newUser.Password, "customer");
				
				if (res != null) {
				
					var newCust = new Customer();
					
					newCust.Fullname = newUser.Fullname;
					
					newCust.Username = newUser.UserName;
					newCust.Address = newUser.Addess;
					newCust.Password = newUser.Password;
					newCust.AddCity = newUser.AddCity;
					newCust.Number = newUser.Number;
					
					ViewBag.register ="register";
					
					_db.Customers.Add(newCust);
			     	_db.SaveChanges();
					
			     	
			     	TempData["success"] = "Registered Successfully";
					return RedirectToAction("Login");
					
				}
				ViewBag.message = "Account is already exist!";
			}
			
			
			else{
				
			ViewBag.notmatch="Password not matched";
			
			}
			
			return View();
		   
		
		
		}
		
		
			[HttpGet]
		[Authorize]
		public ActionResult Edit(int Id)
		{
			var custom = _db.Customers.Find(Id);
			if(custom != null)
			{
				ViewBag.Id = Id;
				return View(custom);
			
			}
		 return RedirectToAction("CustomerInfo");
		}
		[HttpPost]
		[Authorize]
		public ActionResult Edit(Customer custom)
		{
			var customer = _db.Customers.Find(custom.Id);
			customer.Fullname = custom.Fullname;
			customer.Address = custom.Address;
			customer.AddCity = custom.AddCity;
		
			_db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();	
			
			return RedirectToAction("CustomerInfo");
		
		}
		
		
		[HttpGet]
	    public ActionResult AdminReg()
		{
		
			return View();
		}
			
			
		[HttpPost]
		public ActionResult AdminReg(RegisterViewModel newUser, string RetypePassword)
		{
			
			if(newUser.Password == RetypePassword) {
				
		     var res = UserAccount.Create(newUser.UserName, newUser.Password, "admin");
				
				if (res != null) {
				
					var newAdmin = new AdminReg();
					
					newAdmin.FirstName = newUser.FirstName;
					newAdmin.LastName = newUser.LastName;
					newAdmin.UserName= newUser.UserName;
					newAdmin.Password = newUser.Password;
					
					
					_db.AdminRegs.Add(newAdmin);
			     	_db.SaveChanges();
			     	
			     	ViewBag.message="Registered Successfully!";
			     	return View();
			     	
					
				}
				ViewBag.messages = "Registration Failed";
			}
			
			
			else{
				
			ViewBag.messages="Password not matched";
			
			}
			
			return View();
		   
		
		
		}
		[HttpGet]
	    public ActionResult Vetowner()
		{
		
			return View();
		}
			
			
		[HttpPost]
		public ActionResult Vetowner(RegisterViewModel newUser, string RetypePassword)
		{
			
			if(newUser.Password == RetypePassword) {
				
		     var res = UserAccount.Create(newUser.UserName, newUser.Password, "owner");
				
				if (res != null) {
				
					var newtVetOwner = new Vetowner();
					newtVetOwner.Fullname = newUser.Fullname;
					
					newtVetOwner.Username= newUser.UserName;
					newtVetOwner.Password = newUser.Password;
					
					_db.Vetowners.Add(newtVetOwner);
			     	_db.SaveChanges();
			     	
			     	ViewBag.message="Registered Successfully!";
			     	return View();
			     	
					
				}
				ViewBag.messages = "Registration Failed";
			}
			
			
			else{
				
			ViewBag.messages="Password not matched";
			
			}
			
			return View();
		   
		
		
		}
		[Authorize]
		[HttpGet]
		public ActionResult ChangePassword()
		{
			return View();
		
		}
		
		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(string oldpass, string newpass, string retypepass)
		{
		
		
			if(newpass == retypepass)
			{
				string username = Session["user"].ToString();
				bool result = UserAccount.ChangePassword(username, oldpass, newpass);
				if(result)
				{
					TempData["mgsAlert"] ="Password successfuly changed.";
					return RedirectToAction("login","account");
				
				}
				
			}
			TempData["mgsAlertfailed"] ="Password change failed.";
			
			return View();
	
		}
		
        [Authorize(Roles="customer")]
		public ActionResult CustomerInfo()
		{
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var customer = _db.Customers.Where(x => x.Username == user).FirstOrDefault();
			
				int customerid = customer.Id;
				
				List<Customer> customerinfo = _db.Customers.Where(x => x.Id == customerid).ToList();
			
				return View(customerinfo);
			}
			return RedirectToAction("Logoff","Account");
		}
		
	}
}