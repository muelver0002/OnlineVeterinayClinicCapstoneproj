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
	/// Description of AnimalsController.
	/// </summary>
	public class AnimalsController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		public ActionResult Index()
		{
			
			if(Session["user"] != null)
			{
			
			var username = Session["user"].ToString();
			var adds = _db.Vetowners.Where(x => x.Username == username).FirstOrDefault();		
			
			int user = adds.Id;
			
			List<Animalsacom> pet = _db.Animalsacoms.Where(x => x.VetId == user).ToList();
			
			return View(pet);   
			}
			
			return RedirectToAction("Logoff", "Account");
		}
		
		
		
		public ActionResult Create()
		{
		    
			return View();
		}
		[HttpPost]
		public ActionResult Create( Animalsacom animal)
		{
		
			
			if(Session["user"] != null)
			{
				var user = Session["user"].ToString();
				var VetId = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
				
				int Id = VetId.Id;
				
				animal.VetId = Id;
				
				
				TempData["animalmsg"] = "text";
				
			  _db.Animalsacoms.Add(animal);
			  _db.SaveChanges();
			  return View();
			
			
			}
			return RedirectToAction("Logoff", "Account");
		}
		
		[HttpGet]
		[Authorize]
		public ActionResult Edit(int Id)
		{
			var animal = _db.Animalsacoms.Find(Id);
			if(animal != null)
			{
				ViewBag.animal = Id;
				return View(animal);
			 
			}
		
		  return RedirectToAction("Index");
		}
		
		[HttpPost]
		[Authorize]
		public ActionResult Edit(Animalsacom animals)
		{  
			var Animals = _db.Animalsacoms.Find(animals.Id);
			
			Animals.Name = animals.Name;
			
			_db.Entry(Animals).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			
			return RedirectToAction("Index");
		
		}
		
		public ActionResult Delete(int Id)
		{
			var animals = _db.Animalsacoms.Find(Id);
			if(animals != null)
			{
				_db.Animalsacoms.Remove(animals);
				_db.SaveChanges();
			
			}
			return RedirectToAction("Index");
		
		}
		
		
	  }
	}
