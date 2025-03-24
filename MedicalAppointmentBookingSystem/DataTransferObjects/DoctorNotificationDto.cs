using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.DataTransferObjects
{
    public class DoctorNotificationDto
    {
        public int DoctorId { get; set; }

        public string Message { get; set; }
    }
}
