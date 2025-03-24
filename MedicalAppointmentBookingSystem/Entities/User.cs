using Microsoft.AspNetCore.Identity;

namespace MedicalAppointmentBookingSystem.Entities
{
    public class User :  IUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string HashPassword { get; set; }

        public Role role { get; set; }




    }
}
