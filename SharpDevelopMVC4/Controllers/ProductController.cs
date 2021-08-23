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
	/// Description of ProductController.
	/// </summary>
	public class ProductController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		[Authorize]
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
			
			var username = Session["user"].ToString();
			var adds = _db.Vetowners.Where(x => x.Username == username).FirstOrDefault();		
			
			int user = adds.Id;
			
			List<Productacom> product = _db.Productacoms.Where(x => x.VetId == user).ToList();
			
			return View(product);   
			}
			
			return RedirectToAction("Logoff", "Account");
		}
	
		public ActionResult Create()
		{
		
			return View();
		
		
		}
		[HttpPost]
		[Authorize]
		public ActionResult Create(Productacom product)
		{
		
		if(Session["user"] != null)
			{
		   var user = Session["user"].ToString();
		   var VetId = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
		    int Id = VetId.Id;
				
			product.VetId = Id;
				
			TempData["productmsg"] ="text";
			
			 _db.Productacoms.Add(product);
		     _db.SaveChanges();
		     return View();
			
			
			}
			return RedirectToAction("Logoff", "Account");
		
		
		}
		[HttpGet]
		[Authorize]
		public ActionResult Edit(int Id)
		{
			var product = _db.Productacoms.Find(Id);
			if(product != null)
			{
				ViewBag.Product = product;
				ViewBag.Id = Id;
				return View(product);
			
			}
			return RedirectToAction("Index");
		
		}
		
		[HttpPost]
		[Authorize]
		public ActionResult Edit(Productacom Product)
		{
			var Prod = _db.Productacoms.Find(Product.Id);
		 
			Prod.Productname = Product.Productname;
			Prod.Brand = Product.Brand;
			Prod.Price = Product.Price;
			
			_db.Entry(Prod).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			
			return RedirectToAction("Index");
		}
		
		[Authorize]
		public ActionResult Delete(int Id)
		{
			var product = _db.Productacoms.Find(Id);
			if(product != null)
			{
				_db.Productacoms.Remove(product);
				_db.SaveChanges();			
			
			}
		
			return RedirectToAction("Index");
		}
	}
	
}