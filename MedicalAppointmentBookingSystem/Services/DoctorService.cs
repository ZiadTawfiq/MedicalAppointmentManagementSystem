using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Repository;

namespace MedicalAppointmentBookingSystem.Services
{
    public class DoctorService(IDoctorRepository doctorRepository)
    {
        public async Task<string> Add_doctor(DoctorDto dto)
        {
             return await doctorRepository.AddDoctor(dto); 
        }
        public async Task<string> Detect_Available_hours(int id , DoctorAvailabilityDto doctorAvailabilitie)
        {
           return await doctorRepository.DetectAvailableDays_Hours(id, doctorAvailabilitie);
        }
    }
}
