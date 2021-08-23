using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
    [Table("OV_Receptionist")]
	public class Receptionist
	{
		public int Id { get; set; }
		
		public int VetId { get; set; }
		
		public string Fullname { get; set; }
		
		public string Username { get; set; }
		
		public string Password {get; set;}
		
		public string Role { get; set;}
	}
}
