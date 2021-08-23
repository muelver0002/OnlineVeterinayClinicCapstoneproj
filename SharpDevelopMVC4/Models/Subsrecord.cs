using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
	[Table("OV_Subsrecord")]
	public class Subsrecord
	{
		public int Id {get; set;}
		
		public int VetId { get; set;}    
		
		public int Cvcode { get; set; }
		
		public int Price { get; set;}
		
		public DateTime? DateAvail { get; set;}
		
		public DateTime? Duedate { get; set;}
		
		public string Cardnum { get; set; }
		
		public string Fullname { get; set;}
		
		public string Email { get; set;}
		
		public string Address { get; set;}
		
		public string City { get; set;}
		
		public string Vetname { get; set;}
			
		public string Cardname { get; set; }
		
        public string Datexpire { get; set; }
        
        public string Yearexpire { get; set;}
               
        public string Terms { get; set; }
        
        public string Subscription { get; set;}
	}
}
