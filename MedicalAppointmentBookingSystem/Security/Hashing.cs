namespace MedicalAppointmentBookingSystem.Security
{
    public static class Hashing
    {
        public static string HashPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }
}
