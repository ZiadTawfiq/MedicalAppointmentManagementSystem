using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class Doctor : User
    {
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public  Specialization?specialization { get; set; }

        public List<DoctorAvailability> DoctorAvailabilities = new List<DoctorAvailability>();

        public List<TimeSlot> TimeSlots = new List<TimeSlot>(); 

        public List<Appointment> appointments { get; set; } = new List<Appointment>();

        public List<DoctorNotification> Notifications { get; set; } = new List<DoctorNotification>();

        


    }
}
