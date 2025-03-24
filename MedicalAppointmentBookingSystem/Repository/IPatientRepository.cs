using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;

namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IPatientRepository
    {
        Task<string> AddPatient(PatientDto dto);

        Task<string> UpdatePatient(int id , PatientDto dto);

        Task DeletePatient(int id);

        Task <Patient> GetPatientById(int id);

        IAsyncEnumerable<Patient> GetAllPatient();
    }
}
