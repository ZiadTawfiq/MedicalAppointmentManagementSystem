using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Repository;

namespace MedicalAppointmentBookingSystem.Services
{
    public class LoginService(IloginRepository repository)
    {
        public async Task<string> LoginDoc(LoginDto dto)
        {
            return await repository.Login_Doctor(dto); 
        }
        public async Task<string>LoginPatient(LoginDto dto)
        {
            return await repository.Login_Patient(dto);
        }
    }
}
