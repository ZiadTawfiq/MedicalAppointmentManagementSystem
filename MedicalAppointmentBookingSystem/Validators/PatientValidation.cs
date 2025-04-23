using FluentValidation;
using MedicalAppointmentBookingSystem.DataTransferObjects;

namespace MedicalAppointmentBookingSystem.Validators
{
    public class PatientValidation:AbstractValidator<PatientDto>
    {
        public PatientValidation()
        {
            RuleFor(_ => _.Email)
                .EmailAddress().WithMessage("Invalid Email Address!");
                
               
        }
    }
}
