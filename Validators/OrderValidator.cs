using FluentValidation;
using FluentValidation.AspNetCore;
using Medical_Store.Models;
namespace Medical_Store.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerId)
                .NotNull().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");
            



        }
    }
}