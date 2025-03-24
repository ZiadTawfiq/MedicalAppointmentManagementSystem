using MedicalAppointmentBookingSystem.configurations;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentBookingSystem.Controllers
{
    [ApiController]
    [Route("/Doctor/")]
    public class DoctorController(AppDbContext _Context) : ControllerBase
    {

    }
}
