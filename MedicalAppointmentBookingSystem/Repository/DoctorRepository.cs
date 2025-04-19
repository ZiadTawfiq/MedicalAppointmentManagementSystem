using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;
using MedicalAppointmentBookingSystem.Security;
using Microsoft.AspNetCore.Http.HttpResults;
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
                SpecializationId = dto.SpecializationId,
                
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

        public async Task<string> DetectAvailableDays_Hours( int id , DoctorAvailabilityDto availabilitie)
        {
            var doctor = await _context.Doctors.Include(_ => _.DoctorAvailabilities).FirstOrDefaultAsync(_ => _.Id == id);

            if (doctor == null)
            {
                return "Doctor not found!";
            }

            
            bool IsDublicate =  doctor.DoctorAvailabilities.Any(_ => _.Day == availabilitie.Day && _.AvailableStartAT == availabilitie.AvailableStartAT
                  && _.AvailableEndAt == availabilitie.AvailableEndAt
                );
            
            bool IsOverlapping = doctor.DoctorAvailabilities.Any(_ => _.Day == availabilitie.Day &&
                 availabilitie.AvailableStartAT < _.AvailableEndAt  && availabilitie.AvailableEndAt > _.AvailableStartAT
                );

            
            if (IsDublicate)
            {
                return "IsDublicate!";
            }
            if (IsOverlapping)
            {
                return "Overlapping";
            }
            var availability = new DoctorAvailability()
            {
                Day = availabilitie.Day,
                AvailableStartAT = availabilitie.AvailableStartAT,
                AvailableEndAt = availabilitie.AvailableEndAt,
                DocId = id,
                IsAvailable = true
            };
            doctor.DoctorAvailabilities.Add(availability);
            await _context.SaveChangesAsync();
            await GenerateTimeSlots(id, availability.Id, 40);
            return "Availability Added Succefully";
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
                return new Doctor();
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
            doctor.SpecializationId= dto.SpecializationId;
            doctor.Email = dto.Email;
            

            return "Updated Succefully";
        }

        public async Task<List<DoctorAvailability>> DisplayAvailableHours_days(int docid) 
        {
            var Doctor = await _context.Doctors.Include(_ => _.DoctorAvailabilities).ThenInclude(_=>_.TimeSlots).FirstOrDefaultAsync(_ => _.Id == docid);

            if (Doctor == null)
            {
                return new List<DoctorAvailability>();
            }

            return  Doctor.DoctorAvailabilities.Where(_ => _.IsAvailable).ToList(); 
            
        }

        public async Task<string> Update_Availability(int doctorId, int DoctorAvailId, DoctorAvailabilityDto doctorAvailability)
        {
            var doctor = await _context.Doctors.Include(_ => _.DoctorAvailabilities).FirstOrDefaultAsync(_ => _.Id == doctorId);
            
            if (doctor == null)
            {
                return "Doctor not found!";
            } 

            var Available = doctor.DoctorAvailabilities.FirstOrDefault(_ => _.Id == DoctorAvailId);

            if (Available == null)
            {
                return "Availabl null"; 
            }

            bool IsDublicate = doctor.DoctorAvailabilities.Any(_ => _.Day == doctorAvailability.Day && _.AvailableStartAT == doctorAvailability.AvailableStartAT
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
                _context.TimeSlots.RemoveRange(Available.TimeSlots);
                await GenerateTimeSlots(doctorId,Available.Id, 20);
                await _context.SaveChangesAsync();
                return "Updated Succefully.";

            }
            return "sorry ! overlapping or duplicate Avaialabl hours"; 
           

        }

        public async Task GenerateTimeSlots(int docId,int doctorAvailabilityId, int slotDurationMinutes)
        {
            var availability = await _context.doctorAvailabilities.Include(_ => _.TimeSlots).FirstOrDefaultAsync
                 (_ => _.Id == doctorAvailabilityId);
            if (availability == null)
            {
                throw new Exception("Doctor Availability not Found!");
            }
            var Period = availability.AvailableEndAt - availability.AvailableStartAT;

            int tolalMinutes = (int)(Period.TotalMinutes);

            int Slots = tolalMinutes / slotDurationMinutes;

            for (int i = 0; i < Slots; i++)
            {
                TimeSpan SlotStart  = availability.AvailableStartAT + TimeSpan.FromMinutes(i * slotDurationMinutes);
                TimeSpan SlotEnd = SlotStart+ TimeSpan.FromMinutes(slotDurationMinutes);

                bool slotExists = await _context.TimeSlots
                .AnyAsync(ts => ts.AvailableId == doctorAvailabilityId &&
                           ts.StartTime == SlotStart &&
                           ts.EndTime == SlotEnd);

                if (!slotExists)
                {
                    var slot = new TimeSlot()
                    {
                        StartTime = SlotStart,
                        EndTime = SlotEnd,
                        IsAvailable = true , 
                        AvailableId = availability.Id ,
                        DoctorId = docId
                    };
                    availability.TimeSlots.Add(slot);
                }

            }
            await _context.SaveChangesAsync();


        }

        public async IAsyncEnumerable<TimeSlot>GetAvailableTimeSlots(int doctorId, TimeSpan Time , DayOfWeek day)
        {
            var timeSlots = _context.TimeSlots.Include(_ => _.DoctorAvailability).Where(_ => _.DoctorAvailability.DocId == doctorId && _.DoctorAvailability.Day == day && _.StartTime== Time && _.IsAvailable == true
            );
            await foreach (var slot in timeSlots.AsAsyncEnumerable())
                yield return slot;
        }
    }


   
}

