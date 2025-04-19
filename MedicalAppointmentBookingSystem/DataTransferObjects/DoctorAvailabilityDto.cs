using System.Text.Json.Serialization;

namespace MedicalAppointmentBookingSystem.DataTransferObjects
{
    public class DoctorAvailabilityDto
    {
        public int DocId { get; set; }
       
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; set; }

        public TimeSpan AvailableStartAT { get; set; }

        public TimeSpan AvailableEndAt { get; set; }

    }
}
