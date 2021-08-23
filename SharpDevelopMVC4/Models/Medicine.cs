using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SharpDevelopMVC4.Models
{
	[Table("OV_Medicine")]
	public class Medicine
	{
		public int Id { get; set; }
		
		public int VetId { get; set; }
		
		public int Price  { get; set; }
		
		public string Name { get; set; }
		
		public string Brand { get; set; }
	}
}
