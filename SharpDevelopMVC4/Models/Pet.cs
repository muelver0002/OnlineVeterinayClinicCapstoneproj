using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SharpDevelopMVC4.Models
{
	[Table("OV_Pet")]
	public class Pet
	{
		
		public int Id { get; set;}
		public int OwnersID {get; set;}
		public string PetName { get; set; }
		public string OwnersName {get; set;}
		public string Breed { get; set; }
		public string Type { get; set; }
		public string Color { get; set; }
		public string Bloodtype { get; set; }
		public string Gender { get; set; }
		public string Bdate { get; set; }
		
		
	}
}
