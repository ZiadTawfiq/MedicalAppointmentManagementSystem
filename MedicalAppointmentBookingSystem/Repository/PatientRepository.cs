using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Security;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class PatientRepository(AppDbContext _context) : IPatientRepository
    {
        public async Task<string> AddPatient(PatientDto dto)
        {
            bool found = await _context.Patients.AnyAsync(_ => _.Email == dto.Email);

            if (found)
            {
                return "Sorry!this user already found";
            }
            var patient = new Patient()
            {
                DateOfBirth = dto.DateofBirth,
                Email = dto.Email,
                MobilePhone = dto.MobilePhone,
                Name = dto.Name,
                HashPassword = Hashing.HashPassword(dto.password),
                role = dto.role

            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return "Saved Successfully";
        }

        public async Task DeletePatient(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);

            if (patient == null)
            {
                return;
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

        }

        public IAsyncEnumerable<Patient> GetAllPatient()
        {
            return _context.Patients.AsAsyncEnumerable();

        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);

        }

        public async Task<string> UpdatePatient(int id , PatientDto dto)
        {
            var patient =  await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id); ;

            if (patient == null)
            {
                return "Not Found";
            }
            patient.DateOfBirth = dto.DateofBirth;
            patient.Email = dto.Email;
           
            return "Updated Succefully";
        }

       
    }
}
