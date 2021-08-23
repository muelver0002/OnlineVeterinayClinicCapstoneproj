using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SharpDevelopMVC4.Models
{
	[Table("OV_Rerturncheckup")]
	public class Returncheckup
	{
		public int Id { get; set; }
		
		public int Vetid { get; set; }
		
		public int CustId { get; set; }
		
		public string Vetname { get; set;}
		
		public string DocName { get; set;}
		
		public string Customername { get; set; }
		
		public string Number { get; set; }
		
		public string Adrress { get; set; }
		
		public string Concern { get; set; }
		
		public string Petname {get; set;}
		
		public string Pettype { get; set; }
		
		public string PetBreed { get; set; }
		
		public string Petcolor { get; set; }
		
		public string Bloodtype { get; set; }
		
		public string Gender { get; set; }
		
		public string Bdate { get; set; }
		
		public string Petage { get; set; }
		
		
		public string Weight { get; set; }
		
		public string Temperature { get; set; }
		
		public DateTime? Datetoday { get; set; }
		
		public string Complain { get; set; }
		
		public string Prescription { get; set;}
		
		public string Diagnoselater {get; set;}
		
		public string Followupdate { get; set;}
		
		public string DateStart { get; set;}
		
		public string DateEnd { get; set;}
	}
}
