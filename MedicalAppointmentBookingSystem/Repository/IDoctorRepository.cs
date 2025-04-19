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

        Task<string> DetectAvailableDays_Hours(int id ,DoctorAvailabilityDto doctorAvailabilitie);

        Task<List<DoctorAvailability>>DisplayAvailableHours_days(int id);

        Task<string> Update_Availability(int doctorId, int DoctorAvailId, DoctorAvailabilityDto dto);
       
        Task GenerateTimeSlots(int Docid,int doctorAvailabilityId, int slotDurationMinutes);

        IAsyncEnumerable<TimeSlot> GetAvailableTimeSlots(int doctorId, TimeSpan time , DayOfWeek day); 



    }

}
