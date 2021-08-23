using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharpDevelopMVC4.Models
{
	[Table("OV_ADDRESS")]
	public class Address
	{
		public int Id { get; set; }
		
		public string Addname { get; set; }
	}
}
