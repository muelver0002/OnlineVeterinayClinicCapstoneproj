using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharpDevelopMVC4.Models
{
	[Table("OV_Subscription")]
	public class Subscription
	{
		public int Id { get; set; }
		
		public int Price { get; set;}
		
		public string Subname { get; set;}

	}
}
