using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpDevelopMVC4.Models;

namespace SharpDevelopMVC4.Controllers
{
	
	public class PaymentController : Controller
	{
		SdMvc4DbContext _db = new SdMvc4DbContext();
		
		[Authorize]
		public ActionResult Index()
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner"))
				{
					var user = Session["user"].ToString();
					var userinfo = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
					int userId = userinfo.Id;	
					
					var userexist = _db.Payments.Where(x => x.VetId == userId).FirstOrDefault();
                   
					if(userexist != null)
					{  
						List<Payment> payments = _db.Payments.Where(x => x.VetId == userId).ToList();
						ViewBag.msg = userId;
						return View(payments);					
					}
					if(userexist == null){
						
						TempData["pays"] = "msg";
						return RedirectToAction("Create");					
					}
				}				
				if(User.IsInRole("admin"))
				{
				
					List<Payment> viewpayment = _db.Payments.ToList();
					return View(viewpayment);
				}
			
			}
			
			return RedirectToAction("Logoff","Account");
		}
		
		
		[Authorize]
		[HttpGet]
		public ActionResult Create()
		{
			List<Subscription> subs = _db.Subscriptions.ToList();
			ViewBag.subss = subs;
			return View();
		}
		[Authorize]
		[HttpPost]
		public ActionResult Create(Payment payment, int Subscription)
		{
			if(Session["user"] != null)
			{
				if(User.IsInRole("owner"))
				{
//					ViewBag.datenow = DateTime.Now.Date;
					
					var user = Session["user"].ToString();
					var userinfo = _db.Vetowners.Where(x => x.Username == user).FirstOrDefault();
					int UserId = userinfo.Id;
					string Vetname = userinfo.Fullname;
					
					var userexist = _db.Payments.Where(x => x.VetId == UserId).FirstOrDefault();
					if(userexist != null)
					{					  
					 return RedirectToAction("Index","Home");
					}
					
					var Subs = _db.Subscriptions.Where(x => x.Id == Subscription).FirstOrDefault();
					string payname = Subs.Subname;
					int price = Subs.Price;
					
					payment.Price = price;
					payment.Subscription = payname;
					payment.DateAvail = DateTime.Now.Date;
					payment.Vetname = Vetname;
					payment.VetId = UserId;
					_db.Payments.Add(payment);
					_db.SaveChanges();
					TempData["payment"] = "Your application has been sent please wait for admins verification and approval. Thank You...";
					return RedirectToAction("Index","Home");
				}
			}
			return RedirectToAction("Logoff","Account");
		}
		[Authorize]
		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var payment = _db.Payments.Find(Id);
			if(payment != null)
			{
				ViewBag.Payment = payment;
				ViewBag.Id = Id;
				List<Subscription> subs = _db.Subscriptions.ToList();
			    ViewBag.subss = subs;
				return View(payment);
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		[Authorize]
		public ActionResult Edit(Payment payment,int Subscription)
		{
			var Payment = _db.Payments.Find(payment.Id);
			
			Payment.Fullname = payment.Fullname;
			
			Payment.Address = payment.Address;
			
			Payment.Email = payment.Email;
			
            Payment.City = payment.City;
			
			Payment.Cardname = payment.Cardname;
			
			Payment.Cardnum = payment.Cardnum;
			
			Payment.Cvcode = payment.Cvcode;
			
			Payment.Datexpire = payment.Datexpire;
			
			Payment.Yearexpire = payment.Yearexpire;
			
			Payment.Duedate = payment.Duedate;
			
			var Subs = _db.Subscriptions.Where(x => x.Id == Subscription).FirstOrDefault();
			string payname = Subs.Subname;
			int price = Subs.Price;
			Payment.DateAvail = DateTime.Now.Date;
			
			Payment.Subscription = payname;
			
			Payment.Price = price;
			
			_db.Entry(Payment).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();

			
			
			return RedirectToAction("Index");
		}
		
		[Authorize]
		public ActionResult Delete(int Id)
		{
			Payment pay = _db.Payments.Find(Id);
			if(pay != null)
			{
				_db.Payments.Remove(pay);
				_db.SaveChanges();
			
			}
		   return RedirectToAction("Index");
		
		}
	}
}