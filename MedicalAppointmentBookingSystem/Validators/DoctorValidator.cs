using FluentValidation;
using MedicalAppointmentBookingSystem.DataTransferObjects;
using MedicalAppointmentBookingSystem.Entities;

namespace MedicalAppointmentBookingSystem.Validators
{
    public class DoctorValidator:AbstractValidator<DoctorDto>
    {
        public DoctorValidator()
        {
            RuleFor(_ => _.Email)
                .NotEmpty().WithMessage("Email is Required!")
                .EmailAddress().WithMessage("صيغه غيرصحيحه");
            RuleFor(_ => _.Name)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(_ => _.MobilePhone)
                .NotEmpty()
                .Matches(@"^(?:\+201|01)[0-2,5]{1}[0-9]{8}").WithMessage("Mobile Phone is not Valid!");
            RuleFor(_ => _.password)
                .MinimumLength(8)
                .Equal(_ => _.ConfirmPassword).WithMessage("Passwords in not Equal!");
                
               
                
        }
    }
}
