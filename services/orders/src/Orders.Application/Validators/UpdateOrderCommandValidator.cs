using FluentValidation;
using Orders.Application.Orders.Commands;

namespace Orders.Application.Validators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
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
