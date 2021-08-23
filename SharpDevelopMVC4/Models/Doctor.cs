using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
	[Table("OV_Doctor")]
	public class Doctor
	{
		public int Id { get; set; }
		
		public int Vetid {get; set;}
		
		public string Fullname { get; set; }
		
		public string Username { get; set; }
		
		public string Password { get; set; }
	}
}
