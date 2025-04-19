using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class PatientNotification
    {
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient ?patient { get; set; }

        public string Message { get; set; }

        public DateTime sentAt { get; set; } = DateTime.Now;


    }
}
