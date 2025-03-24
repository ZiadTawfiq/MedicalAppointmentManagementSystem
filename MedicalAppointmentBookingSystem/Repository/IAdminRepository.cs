using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IAdminRepository
    {

        Task<string> AddPatient(PatientDto dto);
        
        Task DeletePatient(int id);
        
        IAsyncEnumerable<Patient> GetAllPatient();
      
        Task<Patient> GetPatientById(int id);

        Task<string> UpdatePatient(int id ,PatientDto dto);
       
    }
}
