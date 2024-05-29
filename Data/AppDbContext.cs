using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Dtos.CreateAndEditRequests;
using Dental_Clinic.Dtos.GetResponse;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dental_Clinic.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Invoice> Invoices {  get; set; }
        public DbSet<Appointment> Appointments {  get; set; }
        public DbSet<MedicalHistory> MedicalHistories {  get; set; }
        public DbSet<DentalHistory> DentalHistories {  get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Patient>().HasIndex(p => p.Mobile).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

    }
}
