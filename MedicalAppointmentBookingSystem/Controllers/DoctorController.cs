using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentBookingSystem.Controllers
{
    [ApiController]
    [Route("/Doctor/")]
    public class DoctorController(DoctorService doctorService, LoginService loginService) : ControllerBase
    {
        [HttpPost]
        [Route("Sign_up")]
        public async Task <ActionResult<string>> add_doctor([FromBody]DoctorDto dto)
        {
            return Ok (await(doctorService.Add_doctor(dto))); 
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginDoctor([FromBody]LoginDto dto)
        {
            return Ok(await loginService.LoginDoc(dto)); 
        }
        [HttpPost]
        [Route("DetectAvailable_Hours_days/{id}")]
        [Authorize(Roles ="Doctor")]
        public async Task<ActionResult<string>> Add_Available_days_Hours([FromRoute]int id ,[FromBody] DoctorAvailabilityDto doctorAvailabilitie)
        {
            return Ok( await( doctorService.Detect_Available_hours(id, doctorAvailabilitie)));
        }

        

    }
}
