using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Security;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class LoginRepository(AppDbContext _context ,TokenService tokenService) : IloginRepository
    {
        public async Task<string> Login_Doctor(LoginDto dto)
        {
            var Doctor = await _context.Doctors.FirstOrDefaultAsync(_ => _.Email == dto.Email); 
            if (Doctor == null)
            {
                return "User Not found!";
            }
            if (Hashing.VerifyPassword(dto.password ,Doctor.HashPassword))
            {

                return tokenService.GenerateToken(Doctor.Id, Doctor.role.ToString()); 

            }

            return "Password is not Correct!";
        }

        public async Task<string> Login_Patient(LoginDto dto)
        {
            var Patient = await _context.Patients.FirstOrDefaultAsync(_ => _.Email == dto.Email);
            if (Patient == null)
            {
                return "User Not found!";
            }
            if (Hashing.VerifyPassword(dto.password , Patient.HashPassword))
            {

                return tokenService.GenerateToken(Patient.Id, Patient.role.ToString());
            }

            return "Password is not Correct!";
        }
    }
}
