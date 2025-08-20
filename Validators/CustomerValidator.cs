using FluentValidation;
using Medical_Store.Models;

namespace Medical_Store.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {

            RuleFor(c => c.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(c => c.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .Length(3, 50).WithMessage("Customer name must be between 3 and 50 characters.")
                .Matches("^[A-Za-z ]+$").WithMessage("Customer name can only contain letters and spaces.");

            RuleFor(c => c.ContactNumber)
                .Matches(@"^\d{10}$").When(c => !string.IsNullOrWhiteSpace(c.ContactNumber))
                .WithMessage("Contact number must be exactly 10 digits.");

            RuleFor(c => c.Address)
                .MaximumLength(100).When(c => !string.IsNullOrWhiteSpace(c.Address))
                .WithMessage("Address must not exceed 100 characters.");

            RuleFor(c => c.City)
                .MaximumLength(50).When(c => !string.IsNullOrWhiteSpace(c.City))
                .WithMessage("City name must not exceed 50 characters.")
                .Matches("^[A-Za-z ]*$").When(c => !string.IsNullOrWhiteSpace(c.City))
                .WithMessage("City name must contain only letters and spaces.");
        }
    }

}
