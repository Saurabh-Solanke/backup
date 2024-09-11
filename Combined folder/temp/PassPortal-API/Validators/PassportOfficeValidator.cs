/*using FluentValidation;
using PassPortal_API.DTOs;

namespace PassPortal_API.Validators
{
    public class PassportOfficeValidator : AbstractValidator<PassportOfficeDTO>
    {
        public PassportOfficeValidator()
        {
            RuleFor(x => x.OfficeName).NotEmpty().WithMessage("Office name is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Contact number is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}
*/