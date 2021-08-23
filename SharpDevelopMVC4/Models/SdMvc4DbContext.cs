using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharpDevelopMVC4.Models
{
    public class SdMvc4DbContext : DbContext
    {
        public SdMvc4DbContext() : base("SdMvcDb") // name_of_dbconnection_string
        {
        }

        //Map model classes to database tables
        public DbSet<UserAccount> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Customer> Customers {get; set;}
        
        public DbSet<AdminReg> AdminRegs {get; set;}
        
        public DbSet<Pet> Pets {get; set;}
        
        public DbSet<Vetowner> Vetowners {get; set;}
        
        public DbSet<Productacom> Productacoms {get; set;}
        
        public DbSet<Servicesacon> Servicesacons {get; set;} 
        
        public DbSet<Animalsacom> Animalsacoms {get; set;}
        
        public DbSet<Appointment> Appointments { get; set; }
        
        public DbSet<Patient> Patients {get; set;}
        
        public DbSet<Doctor> Doctors {get; set;}
        
        public DbSet<Receptionist> Receptionists {get; set;}
        
        public DbSet<Diagnose> Diagnoses {get; set;}
        
        public DbSet<Diagnoserecord> Diagnoserecords { get; set;}
        
        public DbSet<Recept> Recepts { get; set; }
        
        public DbSet<Medicine> Medicines { get; set;}
        
        public DbSet<Address> Addreses { get; set; }
        
        public DbSet<Salesrecord> Salesrecords { get; set; }
        
        public DbSet<Returncheckup> Returncheckups { get; set;}
        
        public DbSet<Payment> Payments { get; set;}
        
        public DbSet<Admission> Admissions { get; set;}
        
        public DbSet<Subscription> Subscriptions { get; set;}
        
        public DbSet<Subsrecord> Subsrecord { get; set;}
    }


}

