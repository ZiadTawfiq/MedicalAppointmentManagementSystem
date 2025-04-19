using MedicalAppointmentBookingSystem.DataTransferObjects;

namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IloginRepository
    {
        
        Task<string> Login_Doctor(LoginDto dto);
        Task<string> Login_Patient(LoginDto dto);
        
    }
}
