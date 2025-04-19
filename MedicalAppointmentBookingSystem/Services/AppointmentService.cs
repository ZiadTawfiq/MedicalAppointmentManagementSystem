using MedicalAppointmentBookingSystem.Repository;

namespace MedicalAppointmentBookingSystem.Services
{
    public class AppointmentService(IAppointmentRepository appointmentRepository)
    {
        public async Task<string> Make_Appointment(int docId , int PatientId, int SlotTimeId) 
        {
            return await appointmentRepository.Book_Appointment(docId, PatientId, SlotTimeId);
        }
        public async Task<string>CancelAppointment(int patientId , int appointmentId)
        {
            return await appointmentRepository.CancelAppointment(patientId, appointmentId);
        }
    }
}
