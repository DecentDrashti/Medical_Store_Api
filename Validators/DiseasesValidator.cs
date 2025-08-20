using FluentValidation;
using Medical_Store.Models;

namespace Medical_Store.Validators
{
    public class DiseasesValidator : AbstractValidator<Disease>
    {
        public DiseasesValidator()
        {

            RuleFor(c => c.DiseaseName)
                .NotEmpty().WithMessage("Diseases name is required.")
                .Matches("^[A-Za-z ]+$").WithMessage("Customer name can only contain letters and spaces.");
        }
    }
}
