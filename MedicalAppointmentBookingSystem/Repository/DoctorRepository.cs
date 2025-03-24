using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Security;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentBookingSystem.Repository
{
    public class DoctorRepository(AppDbContext _context) : IDoctorRepository
    {
        public async Task<string> AddDoctor(DoctorDto dto)
        {
            bool found = await _context.Doctors.AnyAsync(_ => _.Email == dto.Email);

            if (found)
            {
                return "Sorry!this user already found";
            }
            var doctor = new Doctor()
            {
                Email = dto.Email,
                MobilePhone = dto.MobilePhone,
                Name = dto.Name,
                HashPassword = Hashing.HashPassword(dto.password),
                role = dto.role,
                SpechilizationId = dto.specializationId,
                
            };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return "Saved Successfully";
        }

        public async Task DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);

            if (doctor == null)
            {
                return;
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DetectAvailableDays_Hours( int id , List<DoctorAvailability> availabilities)
        {
            var doctor = await _context.Doctors.Include(_ => _.DoctorAvailabilities).FirstOrDefaultAsync(_ => _.Id == id);

            if (doctor == null)
            {
                return; 
            }

            doctor.DoctorAvailabilities ??= new List<DoctorAvailability>(); // if it is null create new instance

            foreach (var item in availabilities)
            {
                bool IsDublicate =  doctor.DoctorAvailabilities.Any(_ => _.Day == item.Day && _.AvailableStartAT == item.AvailableStartAT
                  && _.AvailableEndAt == item.AvailableEndAt
                );
                bool IsOverlapping = _context.doctorAvailabilities.Any(_ => _.Day == item.Day &&
                 item.AvailableStartAT < _.AvailableEndAt  && item.AvailableEndAt > _.AvailableStartAT
                );

                if (!IsDublicate && !IsOverlapping)
                {
                    item.IsAvailable = true;
                    doctor.DoctorAvailabilities.Add(item);
                }

            }
            await _context.SaveChangesAsync();


        }



        public IAsyncEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.AsAsyncEnumerable();
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);

            if (doctor == null)
            {
                return null;
            }
            return doctor;
        }

        public async Task<string> UpdateDoctor(int id, DoctorDto dto)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id); ;

            if (doctor == null)
            {
                return "Not Found";
            }
            doctor.SpechilizationId = dto.specializationId;
            doctor.Email = dto.Email;
            

            return "Updated Succefully";
        }

        public async Task<List<DoctorAvailability>> DisplayAvailableHours_days(int id) 
        {
            var Doctor = await _context.Doctors.Include(_ => _.DoctorAvailabilities).FirstOrDefaultAsync(_ => _.Id == id);

            foreach (var item in Doctor.DoctorAvailabilities)
            {
                if (!item.IsAvailable)
                {
                    Doctor.DoctorAvailabilities.Remove(item); 
                }
            }
            
            return Doctor?.DoctorAvailabilities.ToList()?? new List<DoctorAvailability>();
            
        }

        public async Task<string> Update_Availability(int doctorId, int DoctorAvailId, DoctorAvailability doctorAvailability)
        {
            var patient = await _context.Doctors.Include(_ => _.DoctorAvailabilities).FirstOrDefaultAsync(_ => _.Id == doctorId);
            var Available = patient.DoctorAvailabilities.FirstOrDefault(_ => _.Id == DoctorAvailId);

            bool IsDublicate = patient.DoctorAvailabilities.Any(_ => _.Day == doctorAvailability.Day && _.AvailableStartAT == doctorAvailability.AvailableStartAT
                 && _.AvailableEndAt == doctorAvailability.AvailableEndAt
               );
            bool IsOverlapping = _context.doctorAvailabilities.Any(_ => _.Day == doctorAvailability.Day &&
             doctorAvailability.AvailableStartAT < _.AvailableEndAt && doctorAvailability.AvailableEndAt > _.AvailableStartAT
            );

            if (!IsDublicate && !IsOverlapping)
            {
                Available.Day = doctorAvailability.Day;
                Available.AvailableStartAT = doctorAvailability.AvailableStartAT;
                Available.AvailableEndAt = doctorAvailability.AvailableEndAt;
                Available.IsAvailable = true;
                return "Updated Succefully.";
            }
            return "sorry ! overlapping or duplicate Avaialabl hours"; 
           

        }
    }


   
}

