using MedicalAppointmentBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.configurations
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Patient>Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorNotification>DoctorNotifications { get; set; }
        public DbSet<PatientNotification>PatientNotifications { get; set; }
        public DbSet<DoctorAvailability>doctorAvailabilities { get; set; }
        public DbSet <Specialization>Specializations { get; set; }
        public DbSet<TimeSlot>TimeSlots { get; set;  }

        

        

    }


}
