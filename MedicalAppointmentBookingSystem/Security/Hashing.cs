using BCrypt.Net;

namespace MedicalAppointmentBookingSystem.Security
{
    public static class Hashing
    {
        public static string HashPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
        public static bool VerifyPassword(string password , string Hashed_password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Hashed_password);
        }
    }
}
