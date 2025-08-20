using FluentValidation;
using Medical_Store.Models;
namespace Medical_Store.Validators
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .Length(3, 50).WithMessage("User name must be between 3 and 50 characters.")
                .Matches("^[A-Za-z0-9_]+$").WithMessage("User name can only contain letters, numbers, and underscores.");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(c => c.PasswordHash)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
            RuleFor(c => c.RoleId)
                .GreaterThan(0).WithMessage("Role ID must be greater than 0.")
                .When(c => c.RoleId.HasValue); // Ensure RoleId is checked only if it has a value
            
        }
    }
}
