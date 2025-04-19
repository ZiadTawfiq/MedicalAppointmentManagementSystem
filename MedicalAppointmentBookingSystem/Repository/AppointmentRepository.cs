
using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class AppointmentRepository(AppDbContext _context ,NotificationService notificationService) : IAppointmentRepository
    {
        public async Task<string> Book_Appointment(int patientId, int doctorId, int slotTimeId)
        {

            using var Transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeSlot = await _context.TimeSlots
                    .FirstOrDefaultAsync(_ => _.Id == slotTimeId);

               
                if (timeSlot == null)
                {
                    return "TimeSlot not Found!";
                }

                if (!timeSlot.IsAvailable)
                {
                    return "TimeSlot is not available!";
                }


                var appointment = new Appointment()
                {
                    DoctorId = doctorId,
                    patientId = patientId,
                    SlotId = slotTimeId,
                    status = Status.Booked,
                };

                timeSlot.IsAvailable = false;
                _context.Appointments.Add(appointment);
              
                await _context.SaveChangesAsync();
                await notificationService.Send_email_patient(appointment);
                await notificationService.Send_email_doc(appointment);
                await Transaction.CommitAsync();
             
                return "Booked Successfully";

            }
            catch
            {
                await Transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<string> CancelAppointment(int patientId, int appointmentId)
        {
            var Appointment = await _context.Appointments.Include(_ => _.TimeSlot).ThenInclude(_ => _.DoctorAvailability).FirstOrDefaultAsync(_ => _.Id == appointmentId);

            DateTime CancellationTime = DateTime.Now;

            var startAt = Appointment.dateTime.AddHours(-24);

            Appointment.status = Status.Cancelled;

            if (CancellationTime < startAt)
            {

                Appointment.TimeSlot.IsAvailable = true;

                await _context.SaveChangesAsync();

                return "Cancelled Succefully.";

            }
            else
            {
                await _context.SaveChangesAsync();

                return "U will pay a fee 50 pound sorry!"; 
            }



        }
    }
}
