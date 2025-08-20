using FluentValidation;
using Medical_Store.Models;

public class MedicineValidator : AbstractValidator<Medicine>
{
    public MedicineValidator()
    {

        RuleFor(m => m.MedicineName)
            .NotEmpty().WithMessage("Medicine name is required.")
            .Length(3, 50).WithMessage("Medicine name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9 ]+$").WithMessage("Medicine name must contain only letters, numbers, and spaces.");

        RuleFor(m => m.CompanyId)
            .NotNull().WithMessage("Company must be selected.");

        RuleFor(m => m.CategoryId)
            .NotNull().WithMessage("Category must be selected.");

        RuleFor(m => m.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .Length(3, 50).WithMessage("Manufacturer name must be between 3 and 50 characters.");

        RuleFor(m => m.Type)
            .NotEmpty().WithMessage("Medicine type is required.")
            .Length(2, 30).WithMessage("Type must be between 2 and 30 characters.");

        RuleFor(m => m.MfgDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
            .WithMessage("Manufacturing date cannot be in the future.");

        RuleFor(m => m.ExpiryDate)
            .GreaterThan(m => m.MfgDate)
            .WithMessage("Expiry date must be after the manufacturing date.");

        RuleFor(m => m.Ptr)
            .GreaterThan(0).WithMessage("PTR must be greater than 0.");

        RuleFor(m => m.Mrp)
            .GreaterThan(m => m.Ptr).WithMessage("MRP must be greater than PTR.");

        RuleFor(m => m.Cdpercent)
            .GreaterThanOrEqualTo(0).When(m => m.Cdpercent.HasValue)
            .WithMessage("CD percent must be 0 or positive.");

        RuleFor(m => m.FreeQuantity)
            .GreaterThanOrEqualTo(0).When(m => m.FreeQuantity.HasValue)
            .WithMessage("Free quantity cannot be negative.");

        RuleFor(m => m.IsPrescriptionRequired)
            .NotNull().WithMessage("Prescription requirement must be specified.");
    }
}
