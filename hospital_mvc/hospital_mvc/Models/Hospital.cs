namespace hospital_mvc.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Hospital : DbContext
    {
        // Your context has been configured to use a 'Hospital' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'hospital_mvc.Models.Hospital' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Hospital' 
        // connection string in the application configuration file.
        public Hospital()
            : base("name=Hospital")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Hospital, hospital_mvc.Migrations.Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<AddedCharge> AddedCharges { get; set; }
        

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}