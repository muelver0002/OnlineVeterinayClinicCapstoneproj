using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
	/// <summary>
	/// Description of Sale.
	/// </summary>
	public class Sale
	{
		
		public int Id { get; set; }
		
		public int VetId { get; set; }
		
		public string Product { get; set; }
		
		public int Price { get; set; }
		
		public int Total { get; set; }
		
		
	}
}
