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
            modelBuilder.Entity<Doctor>()
            .HasMany(d => d.DoctorAvailabilities) 
            .WithOne(da => da.Doctor) 
            .HasForeignKey(da => da.DocId) 
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                .HasMany(_ => _.TimeSlots)
                .WithOne(_ => _.Doctor)
                .HasForeignKey(_ => _.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
        }
        public DbSet<Patient>Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorNotification>DoctorNotifications { get; set; }
        public DbSet<PatientNotification>PatientNotifications { get; set; }
        public DbSet<DoctorAvailability>doctorAvailabilities { get; set; }
        public DbSet <Specialization>Specializations { get; set; }
        public DbSet<TimeSlot>TimeSlots { get; set;  }
        public DbSet<Appointment>Appointments { get; set; }

        

        

    }


}
