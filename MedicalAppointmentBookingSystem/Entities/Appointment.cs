using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        [ForeignKey("TimeSlot")]
        public int SlotId { get; set;  }
        public TimeSlot?TimeSlot { get; set; }

        [ForeignKey("Patient")]
        public int patientId { get; set; }
        public  Patient?patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public  Doctor?doctor { get; set; }

        public Status status { get; set; }

        public DateTime dateTime { get; set; }





    }

}
