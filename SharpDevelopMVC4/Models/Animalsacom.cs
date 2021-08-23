using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SharpDevelopMVC4.Models
{
	[Table("OV_Animal")]
	public class Animalsacom
	{
		public int Id {get;  set;}
		
		public int VetId {get; set;}
		
		public string Name {get; set;}
		
		
	}
}
