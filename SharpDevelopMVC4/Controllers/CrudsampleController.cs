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
	/// Description of ProductsController.
	/// </summary>
	public class CrudsampleController : Controller
	{
	private SdMvc4DbContext _db = new SdMvc4DbContext();

	// GET: Products
	public ActionResult Index(string searchString, string seeavail)
		{
		     List<Address> address = _db.Addreses.ToList();
		     ViewBag.Address = address;
			
			 if(User.IsInRole("admin"))
	           {
			 	
	             string status = "Approve";
	            
	             List<Product> res = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
			
					if(res == null)
					{
					ViewBag.Address = address;
					return View();          
					}
					
					ViewBag.Address = address;
					return View(res);   
	         
			   }
			 
			 
			 
			 if(User.IsInRole("owner"))
	           {
			 	
	             string status = "Approve";
	             List<Product> res = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
			
					if(res == null)
					{
					
					return View();          
					}
					ViewBag.Address = address;
					return View(res);   
	         
			   }
			
			
			 
			 
			 
			if(Session["user"] != null)
			 {
			   
							
				var username = Session["user"].ToString();
				var adds = _db.Customers.Where(x => x.Username == username).FirstOrDefault();		
				
				List<Product> Vet = _db.Products.ToList();
				
				string Add = adds.Address;
				string City = adds.AddCity;
				
				//if walay address
				if(Add == null || City == null)
				{
				   ViewBag.message="You donot have an address!";
				   string status = "Approve";
				   List<Product> res = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
			
					if(res == null)
					{
					ViewBag.Address = address;
					return View();          
					}
					ViewBag.Address = address;
					return View(res);    
				
				}
					
				
				  //if ang adress wala nag match
				if(!Vet.Any(x => x.Address.ToLower().Contains(Add.ToLower())))
				{
					
					ViewBag.message="There is no clinic available in your Adress!";

					string status = "Approve";
					List<Product> res = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
			
					if(res == null)
					{
					ViewBag.message="Hello everyone";	
					List<Product> ress = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
					ViewBag.Address = address;
					return View(ress);          
					}
					ViewBag.Address = address;
					return View(res);    
			                
				
				
				    }
			
                     // if Nag match Ang address 
				
					if(Add != null && City != null)
				    {
					string status = "Approve";	
					List<Product> res = _db.Products.Where(x => x.Address.ToLower().Contains(Add.ToLower()) && x.AddCity.ToLower().Contains(City.ToLower())
					                                       && x.Status.ToLower().Contains(status.ToLower()) && x.Stats != "Deactivate").OrderBy(o => o.Name).ToList();
				
					if(res == null)
					{
					  string stats = "Approve";
			         List<Product> ress = _db.Products.Where(x => x.Status.ToLower().Contains(stats.ToLower()) && x.Stats != "Deactivate").ToList();
			         
			         ViewBag.Address = address;
			         return View(ress);
					}
					ViewBag.messages="Clinic's Near you!";
					ViewBag.Adds="See More Availabe Clinic!";
					
					ViewBag.Address = address;
					return View(res);
				
				}
				
				
				
				if(Add != null)
				{
					string stat ="Approve";
					List<Product> res = _db.Products.Where(x => x.Address.ToLower().Contains(Add.ToLower()) || x.AddCity.ToLower().Contains(City.ToLower()) 
				    && x.Status.ToLower().Contains(stat.ToLower()) && x.Stats != "Deactivate").ToList();
				
					if(res == null)
					{
					ViewBag.Address = address;
					 return View();
            
					}
					ViewBag.messages="Clinic's Near you!";
					ViewBag.Adds="See More Availabe Clinic!";
					ViewBag.Address = address;
					
					ViewBag.Address = address;
					return View(res);
				
				   }
				    ViewBag.Address = address;
			    	return View();
			
			       }
					
					return RedirectToAction("Logoff", "Account");
			
            
		}
		
		
		
		
		
			public 	ActionResult Search(string Key)
		    {
		        string status = "Approve";
		        List<Address> address = _db.Addreses.ToList();
		        ViewBag.Address = address;
		        
			if(string.IsNullOrWhiteSpace(Key))
			{  
				List<Product> searchResult = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower()) && x.Stats == "Activate").OrderBy(o => o.Name).ToList();
				 ViewBag.Products = searchResult;
				 
				List<Address> addresses = _db.Addreses.ToList(); 
				ViewBag.Address = addresses;       						
				return View("Index", searchResult);
			
			}
			
			else
			{   
				
				List<Product> searchResult = _db.Products
				.Where(x => x.Name.ToLower()
					       .Contains(Key.ToLower()) && x.Status.ToLower().Contains(status.ToLower()) || x.Address.ToLower().Contains(Key.ToLower()) && x.Stats == "Activate")
					.OrderBy(o => o.Name).ToList();
				
				List<Address> addresss = _db.Addreses.ToList();
				 ViewBag.Address = addresss;
				return View("Index", searchResult);	
		
		}
			
		
		}
		
	
		public 	ActionResult Searches(string Key, string priceSort)
		    {
			string status = "Approve";
			
			if(string.IsNullOrWhiteSpace(Key))
			{
				List<Product> searchResult = _db.Products.Where(x => x.Status.ToLower().Contains(status.ToLower())).OrderBy(o => o.Name).ToList();
				 ViewBag.Products = searchResult;
								        						
				return View("Manage", searchResult);
			
			}
			
			else
			{
				List<Product> searchResult = _db.Products
				.Where(x => x.Name.ToLower()
				.Contains(Key.ToLower()) || x.Address.Contains(Key.ToLower()) && x.Status.ToLower().Contains(status.ToLower()))
				.OrderBy(o => o.Name).ToList();
				
				return View("Manage", searchResult);	
		
		}
	
		}
		
		
		public ActionResult VetOwner()
		{
			var list = _db.Vetowners.ToList();
			return View(list);
		
		}
		
		
		public ActionResult Viewpet()
		{
			var list = _db.Pets.ToList();
			return View(list);
		  
		}
		
		
		public ActionResult ManageCustom()
		{
			var list = _db.Customers.ToList();
			return View(list);
		
		
		}
		
		public ActionResult Admins()
		{
			var lists = _db.AdminRegs.ToList();
			return View(lists);
		
		}
		
		[Authorize]
		public ActionResult Manage(string Status)
		{
			var items = _db.Products.Where(x => x.Status == "Pending").ToList();
			return View(items);            
		}	
		
		[Authorize]
		public ActionResult ManageStats()
		{
		
			var items = _db.Products.Where(x => x.Status == "Approve").ToList();
			return View(items);
		
		}
		[Authorize]
	    public ActionResult Payment()
		{
	    	var payment = _db.Payments.Where(x => x.Duedate == null).OrderByDescending(o => o.Id).ToList();     
	    	var pay = _db.Payments.Where(x => x.Duedate != null).ToList();
	    	if(pay != null)
	    	{
	    		ViewBag.norecord = "No Records...";
	    	
	    	}
			return View(payment);            	
	    }
	    
		[Authorize(Roles="owner")]
		public ActionResult Myinfo()
		{
		
			if(Session["user"] != null)
			{
				var username = Session["user"].ToString();
				var vet = _db.Vetowners.Where(x => x.Username == username).FirstOrDefault();
				int vetId = vet.Id;
				
				List<Product> myvet = _db.Products.Where(x => x.Id == vetId ).ToList();
				
				return View(myvet);
			
			}
            return RedirectToAction("Logoff", "Account");
		
		}

		// GET: Products/Details/5
		[Authorize]
		public ActionResult Details(int? id)
		{
			List<Animalsacom> animals = _db.Animalsacoms.Where(x => x.VetId == id).ToList();
			ViewBag.Animals = animals;
			
			List<Productacom> productacom = _db.Productacoms.Where(x => x.VetId == id).ToList();
			ViewBag.Product = productacom;
			
			List<Servicesacon> services = _db.Servicesacons.Where(x => x.VetId == id).ToList();
			ViewBag.Service = services;
			
			Product product = _db.Products.Find(id);
			
			return View(product);
		}
		
		
		

		// GET: Products/Create
		//[Authorize(Roles = "staff")]
		public ActionResult Create()
		{
			List<Address> address = _db.Addreses.ToList();
		    ViewBag.Address = address;
			var product = new Product();
			
			return View(product);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(RegisterViewModel newUser,string RetypePassword,Vetowner vet, Product product, HttpPostedFileBase fileUpload)
		{
			
			if(newUser.Password == RetypePassword) {
				
		     var res = UserAccount.Create(newUser.UserName, newUser.Password, "owner");
				
			if (res != null) {
				
				
					product.Status = "Pending";
					vet.Username = newUser.UserName;
					vet.Password = newUser.Password;
					vet.Fullname = product.Name;
					if (ModelState.IsValid) {
						if (fileUpload != null)
							product.PictureFilename = fileUpload.SaveAsJpegFile(product.Name);
				
						TempData["ownersucces"] = "text";
						_db.Vetowners.Add(vet);
						_db.SaveChanges();
				
						_db.Products.Add(product);
						_db.SaveChanges();
						TempData["register"] = "Registered Successfully..";
						return RedirectToAction("Login", "Account");
					}
//					ViewBag.messages = "Registration sayup";
					return View(product);
		     	
				}
			ViewBag.messages = "Account already exist!";
				
			}
			
			
			else{
				
			ViewBag.messages="Password not matched";
			
			}
			
			return View(product);
			
			
			
			
			
			
		
		}


		// GET: Products/Edit/5
		// [Authorize(Roles = "staff")]
		public ActionResult Edit(int id)
		{
			Product product = _db.Products.Find(id);

			if (product == null) {
				TempData["msgAlert"] = "Product does not exist.";
				return RedirectToAction("Manage");
			}

			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product updatedProduct, HttpPostedFileBase fileUpload,string Status)
		{   
			if(string.IsNullOrEmpty(Status))
			{
			 updatedProduct.Stats = "Activate";
			 updatedProduct.Status = "Approve";
			}
			
			if(!string.IsNullOrEmpty(Status))
			{
				updatedProduct.Stats =Status;
			    updatedProduct.Status = "Approve";
			}
			_db.Entry(updatedProduct).State = EntityState.Modified;

			if (fileUpload != null) // Update picture
                updatedProduct.PictureFilename = fileUpload.SaveAsJpegFile(updatedProduct.Name);
			else // Retain the current picture
                _db.Entry(updatedProduct).Property(x => x.PictureFilename).IsModified = false;

			_db.SaveChanges();
            if(!string.IsNullOrEmpty(Status))
			{
            	return RedirectToAction("ManageStats");
            }
			    return RedirectToAction("Manage");
		}

		// GET: Products/Delete/5
		// [Authorize(Roles = "staff")]
		public ActionResult Delete(int id)
		{
			Product product = _db.Products.Find(id);
			if (product != null) {
				_db.Products.Remove(product);
				_db.SaveChanges();	           
			} else {
				TempData["msgAlert"] = "Product not found";
			}

			return RedirectToAction("Manage");
		}
	}
}