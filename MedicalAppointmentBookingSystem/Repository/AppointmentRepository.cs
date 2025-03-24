
using MedicalAppointmentBookingSystem.configurations;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class AppointmentRepository(AppDbContext _context) : IAppointmentRepository
    {
        public async Task<string> Book_Appointment(int PatientId, int DoctorId, int SlotTimeId)
        {
            var appointment = await _context.doctorAvailabilities.FirstOrDefaultAsync(_ => _.Id == SlotTimeId && _.DocId == DoctorId);

            
            return;
        }
    }
}
