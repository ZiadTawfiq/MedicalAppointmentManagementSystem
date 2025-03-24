namespace MedicalAppointmentBookingSystem.Entities
{
    public interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string MobilePhone { get; set; }
        string HashPassword { get; set; }
        Role role { get; set;  }

       

    }
}
