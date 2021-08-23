using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SharpDevelopMVC4.Models
{
	[Table("OV_Product")]
	public class Productacom
	{
		public int Id {get; set;}
		 
		public int VetId {get; set;}
		
		public string Productname {get; set;}
		
		public string Brand {get; set;}
		
		public int Price {get; set;}
		
		
	}
}
