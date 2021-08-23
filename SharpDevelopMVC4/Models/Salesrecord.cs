using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SharpDevelopMVC4.Models
{
	[Table("OV_Salesrecord")]
	public class Salesrecord
	{		
		public int Id { get; set; }
		
		public int CusId { get; set; }
		
		public int VetId { get; set; }
		
		public int Qty { get; set; }
		
		public int Price { get; set; }
		
		public int Total { get; set; }
		
		public int ReceptionistId { get; set;}
		
		public int DoctorId { get; set;}
		[Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")] 
		public DateTime? Date { get; set; }
		
		
		
		public string CustName { get; set; }
		
		public string PName { get; set; }
	}
}
