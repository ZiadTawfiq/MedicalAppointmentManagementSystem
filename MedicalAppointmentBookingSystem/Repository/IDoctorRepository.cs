using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;

namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IDoctorRepository
    {


        Task<string> AddDoctor(DoctorDto dto);

        Task<string> UpdateDoctor(int id, DoctorDto dto);

        Task DeleteDoctor(int id);

        Task<Doctor> GetDoctorById(int id);

        IAsyncEnumerable<Doctor> GetAllDoctors();

        Task DetectAvailableDays_Hours(int id , List<DoctorAvailability>doctorAvailabilities);

        Task<List<DoctorAvailability>>DisplayAvailableHours_days(int id);

        Task<string> Update_Availability(int doctorId, int DoctorAvailId, DoctorAvailability doctorAvailability);
       
        Task GenerateTimeSlots(int doctorAvailabilityId, int slotDurationMinutes);
        
        Task<List<TimeSlot>> GetAvailableTimeSlots(int doctorId, DateTime date); 



    }

}
