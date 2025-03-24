using System.Diagnostics.Contracts;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor>?Doctors { get; set; }

    }
}