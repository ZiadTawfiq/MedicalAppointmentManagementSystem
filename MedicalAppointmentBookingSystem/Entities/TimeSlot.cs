using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }
        [ForeignKey("DoctorAvailability")]
        public int AvailableId { get; set;  }
        public DoctorAvailability?DoctorAvailability { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor ?Doctor { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set;  }

        public bool IsAvailable { get; set; }

    }
}
