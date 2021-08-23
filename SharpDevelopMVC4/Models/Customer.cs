using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
	
	[Table("OV_Customer")]
	public class Customer
	{

		public int Id { get; set; }
		public string Fullname { get; set; }
		public string Address { get; set; }
		public string AddCity {get; set;}
		public string Username { get; set; }
		public string Number {get; set;}
		public string Password { get; set; }
		public string RetypePassword { get; set; }
	}
}
