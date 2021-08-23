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
	/// Description of PetController.
	/// </summary>
	public class PetController : Controller
	{
        SdMvc4DbContext _db = new SdMvc4DbContext();
       
		public ActionResult Index()
		{  
			if(Session["user"] != null)
			{
			
			var username = Session["user"].ToString();
			var adds = _db.Customers.Where(x => x.Username == username).FirstOrDefault();		
			
			int user = adds.Id;
			
			List<Pet> pet = _db.Pets.Where(x => x.OwnersID == user).ToList();
			
			return View(pet);   
			}
			
			return RedirectToAction("Logoff", "Account");
		}
		
		
		public ActionResult Create()
		{
			
			
			return View();
		}
		
		[HttpPost]
		public ActionResult Create(Pet pets )
		{
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var Owner = _db.Customers.Where(x => x.Username == user).FirstOrDefault();
				
				int Id = Owner.Id;
				
				pets.OwnersID = Id;
				
				
				
			_db.Pets.Add(pets);
			_db.SaveChanges();
			
			TempData["pet"] = "hello";
			return RedirectToAction("Index");
			
			
			}
			return RedirectToAction("Logoff", "Account");
		}
		
		
		
		public ActionResult Delete(int Id)
		{
			var p = _db.Pets.Find(Id);
			
			if(p !=null)
			{
				_db.Pets.Remove(p);
				_db.SaveChanges();
				TempData["deletpet"] ="deletpet";
			}
			
			return RedirectToAction("Index");
		}
		
		
		
		public ActionResult Edit(int Id)
		 {
		  var p = _db.Pets.Find(Id);
		  
		 if(p !=null)
		  {
		    	    
		    ViewBag.Product = p;		    	
		    ViewBag.ID = Id;
		    
			return View(p);
			
			 }
			
			else
			{
				return RedirectToAction("Index");
			}
		
		}
		
		
			[HttpPost]
			public ActionResult Edit(Pet updatePet)
			{
				
				var p =_db.Pets.Find(updatePet.Id);
				
				
				p.PetName = updatePet.PetName;
//				p.OwnersName = updatePet.OwnersName;
				p.Breed = updatePet.Breed;
				p.Type = updatePet.Type;
				p.Color = updatePet.Color;
				p.Bloodtype = updatePet.Bloodtype;
				p.Gender = updatePet.Gender;
				p.Bdate = updatePet.Bdate;
				_db.Entry(p).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
			   
				TempData["edit"] ="success";
				
			   return RedirectToAction("Index");
			
			}
			
	}
}