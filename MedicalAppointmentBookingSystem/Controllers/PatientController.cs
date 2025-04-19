using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Repository;
using MedicalAppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentBookingSystem.Controllers
{
    [ApiController]
    [Route("/patient/")]
    public class PatientController(PatientService patientService ,IloginRepository LoginRepository) :ControllerBase
    {
        [HttpPost]
        [Route("sign_up")]
        public async Task <IActionResult> AddPatient(PatientDto dto)
        {
            return Ok(await patientService.Add_patient(dto));
        }
       
        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> Login(LoginDto dto)
        {
            return Ok(await LoginRepository.Login_Patient(dto));
        }

    }
}
