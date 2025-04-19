namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IAppointmentRepository
    {
        Task<string> Book_Appointment(int PatientId, int DoctorId , int SlotTimeId);
        Task<string> CancelAppointment(int patientId, int appointmentId);
    }
}
