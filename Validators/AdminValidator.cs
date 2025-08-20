using FluentValidation;
using FluentValidation.AspNetCore;
using Medical_Store.Models;
namespace Medical_Store.Validators
{
    public class AdminValidator:AbstractValidator<Admin>
    {
        public AdminValidator() {
            RuleFor(c => c.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("Admin name is required.")
                .Length(3, 50).WithMessage("Admin name must be between 3 and 50 characters.")
                .Matches("^[A-Za-z ]+$").WithMessage("Admin name can only contain letters and spaces.");
            
        }
    }
}
