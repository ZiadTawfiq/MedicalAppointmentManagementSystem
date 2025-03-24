namespace MedicalAppointmentBookingSystem.Repository
{
    public interface IAppointmentRepository
    {
        Task<string> Book_Appointment(int PatientId, int DoctorId , int SlotTimeId);
    }
}
