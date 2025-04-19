using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Repository;

namespace MedicalAppointmentBookingSystem.Services
{
    public class NotificationService(INotificationRepository notificationRepository)
    {
        public async Task<string>Send_email_doc(Appointment app)
        {
            return await notificationRepository.Send_Mail_Notification_Doctor(app); 
        }
        public async Task<string>Send_email_patient(Appointment app)
        {
            return await notificationRepository.Send_Mail_Notification_Patient(app);
        }
    }
}
