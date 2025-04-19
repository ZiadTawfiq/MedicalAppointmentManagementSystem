using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;

namespace MedicalAppointmentBookingSystem.Repository
{
    public interface INotificationRepository
    {
        Task<string> Send_Mail_Notification_Doctor(Appointment appointment);

        Task<string> Send_Mail_Notification_Patient( Appointment appointment ); 
        
    }
}
