using MedicalAppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentBookingSystem.Controllers
{
    [ApiController]
    [Route("/Appointment/")]
    public class AppointmentController(AppointmentService appointmentService):ControllerBase
    {
        [HttpPost]
        [Route("Create/{patientId}/{Doctorid}/{timeslotid}")]
        [Authorize(Roles ="Patient")]
        public async Task<IActionResult> Make_appointment(int patientId  , int Doctorid , int timeslotid)
        {
            return Ok(await appointmentService.Make_Appointment(patientId, Doctorid, timeslotid)); 
        }
        [HttpDelete]
        [Route("Cancel")]
        [Authorize("Patient")]
        public async Task <IActionResult> Cancel_appointment(int patientId , int appointment_id)
        {
            return Ok(await appointmentService.CancelAppointment(patientId, appointment_id)); 
        }
    }
}
