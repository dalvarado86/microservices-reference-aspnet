using FluentValidation;
using Orders.Application.Orders.Commands;

namespace Orders.Application.Validators
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.Order.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(p => p.Order.EmailAddress)
               .NotEmpty()
               .EmailAddress();

            RuleFor(p => p.Order.TotalPrice)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
