using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SharpDevelopMVC4.Models
{

	[Table("OV_AdminList")]
	public class AdminReg
	{	
		
	public int Id { get; set; }	
	public string UserName { get; set; }
	public string Password { get; set; }
	
	
	// Add you Registration fields here...
	public string LastName { get; set; }
	public string FirstName { get; set; }
	
	}
}
