using MedicalAppointmentBookingSystem.Entities;
using System.Text.Json.Serialization;

namespace MedicalAppointmentBookingSystem.DataTransferObjects
{
    public class PatientDto
    {
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role role { get; set; }
        public string MobilePhone { get; set; }
        public string password { get; set; }
    }
}
