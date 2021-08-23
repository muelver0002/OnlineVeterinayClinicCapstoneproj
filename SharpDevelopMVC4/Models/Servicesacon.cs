using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace SharpDevelopMVC4.Models
{
	[Table("OV_Service")]
	public class Servicesacon
	{
		
		public int Id {get; set;}
		
		public int VetId {get; set;}
		
		public string Servicesname {get; set;}
		
		public int Price {get; set;}
		
	}
}
