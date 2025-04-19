using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class Patient  : User
    {
        public DateTime DateOfBirth { get; set; }

        public List<Appointment>? appointments{ get; set;  }

        public List<PatientNotification>? Notifications { get; set; }

      
    }
}
