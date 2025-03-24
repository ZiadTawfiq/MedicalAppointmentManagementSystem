using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class TimeSlot
    {
        [ForeignKey("DoctorAvailability")]
        public int AvailableId { get; set;  }
        public DoctorAvailability?DoctorAvailability { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set;  }

        public bool IsAvailable { get; set; }

    }
}
