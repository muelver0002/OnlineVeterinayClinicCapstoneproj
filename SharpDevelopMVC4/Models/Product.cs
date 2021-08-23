using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopMVC4.Models
{
    [Table("OF_Products")] // Sets the name of the table in the database
    public class Product
    {
        public int Id { get; set; }       
        public string Name { get; set; }      
        public string Address {get; set;}
        public string AddCity {get; set;}
        public string contactInfo {get; set;}
        
        public string Username {get; set;}
		public string Password {get; set;}
		public string RetypePassword {get; set;}
        
        public string Cardname { get; set; }
        
        public int Cardnum { get; set; }
        
        public string Datexpire { get; set; }
        
        public int Cvcode { get; set; }
        
        public string Terms { get; set; }
         
        public string Status {get; set;}

        public string Stats { get; set;}
        
        public string Unit { get; set; }
        
        public string Description { get; set;}
        
        public string PictureFilename { get; set; } // Save as image file on disk
        
    }
}
