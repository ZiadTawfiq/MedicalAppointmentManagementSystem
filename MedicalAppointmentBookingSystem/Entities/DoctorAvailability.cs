using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class DoctorAvailability
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public int DocId { get; set; }
        public Doctor? Doctor { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; set; }

        public DateTime AvailableStartAT { get; set; }

        public DateTime AvailableEndAt { get; set;  }

        public bool IsAvailable { get; set; }

        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }
}