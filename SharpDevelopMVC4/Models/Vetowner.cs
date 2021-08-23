using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SharpDevelopMVC4.Models
{
	
	[Table("OV_VetOwner")]
	public class Vetowner
	{
		public int Id {get; set;}
		public string Fullname {get; set;}
		public string Username {get; set;}
		public string Password {get; set;}
		public string RetypePassword {get; set;}
		
		
		
	}
}
