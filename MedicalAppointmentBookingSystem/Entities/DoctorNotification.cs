using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class DoctorNotification
    {
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set;  }

        public string Message { get; set; }

        public DateTime sentAt { get; set; } = DateTime.Now;

    }
}
