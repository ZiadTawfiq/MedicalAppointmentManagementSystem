using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Repository;

namespace MedicalAppointmentBookingSystem.Services
{
    public class PatientService(IPatientRepository patientRepository)
    {
        public async Task<string> Add_patient(PatientDto dto)
        {
            return await patientRepository.AddPatient(dto); 
        }
    }
}
