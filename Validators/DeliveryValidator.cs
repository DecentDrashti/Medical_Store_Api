using FluentValidation;
using FluentValidation.AspNetCore;
using Medical_Store.Models;
namespace Medical_Store.Validators
{
    public class DeliveryValidator : AbstractValidator<Delivery>
    {
        public DeliveryValidator()
        {
            RuleFor(c => c.BillId)
                .GreaterThan(0).WithMessage("Delivery ID must be greater than 0.");
            RuleFor(c => c.CustomerId)
                .GreaterThan(0).WithMessage("Order ID must be greater than 0.");
            //RuleFor(c => c.DeliveryDate)
            //    .NotEmpty().WithMessage("Delivery date is required.")
            //    .Must(date => date.Date >=DateOnly.Now).WithMessage("Delivery date must be today or a future date.");

            RuleFor(c => c.DeliveryStatus)
                .NotEmpty().WithMessage("Delivery status is required.")
                .Matches("^(Pending|Shipped|Delivered|Cancelled)$").WithMessage("Delivery status must be one of the following: Pending, Shipped, Delivered, Cancelled.");

        }
    }
}
