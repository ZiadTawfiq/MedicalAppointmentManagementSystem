using MailKit.Net.Smtp;
using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class NotificationRepository(AppDbContext _context) : INotificationRepository
    {
        public async Task<string> Send_Mail_Notification_Doctor(Appointment appointment)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == appointment.DoctorId);

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Shifaa", "ziadtawfik852@gmail.com"));

            email.To.Add(new MailboxAddress(doctor.Name, doctor.Email));

            email.Subject = "Appointment Notification";

            email.Body = new TextPart("plain")
            {
                Text = $"Dear Dr. {doctor.Name} , \n\n You have an Appointment Scheduled At {appointment.dateTime} "
            };

            using var smtp = new SmtpClient();

            try
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("ziadtawfik852@gmail.com", "pans xfkn ahhw pqhc");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                var notification = new DoctorNotification()
                {
                    DoctorId = appointment.DoctorId,
                    Message = email.Body.ToString(),

                };
                await _context.DoctorNotifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                return "Sent Succefully";
            }
            catch (Exception ex)
            {
                return $"Error Sending email :  {ex.Message}";
            }
        }

        public async Task<string> Send_Mail_Notification_Patient(Appointment appointment)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(_ => _.Id == appointment.patientId);

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Shifaa", "ziadtawfik852@gmail.com"));

            email.To.Add(new MailboxAddress(patient.Name, patient.Email));

            email.Subject = "Appointment Notification";

            email.Body = new TextPart("plain")
            {
                Text = $"Dear. {patient.Name} , \n\n You have an Appointment Scheduled At {appointment.dateTime} "
            };

            using var smtp = new SmtpClient();

            try
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("ziadtawfik852@gmail.com", "pans xfkn ahhw pqhc"
);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                var notification = new PatientNotification()
                {
                    PatientId = appointment.patientId,
                    Message = email.Body.ToString(),
                    
                };
                await _context.PatientNotifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                return "Sent Succefully";
            }
            catch (Exception ex)
            {
                return $"Error Sending email :  {ex.Message}";
            }

        }
    }
}
