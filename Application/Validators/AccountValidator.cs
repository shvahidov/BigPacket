namespace Application.Validators;

using Domain.Entities;
using FluentValidation;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x.OwnerName)
            .NotEmpty().WithMessage("Owner name is required")
            .MaximumLength(100).WithMessage("Owner name cannot exceed 100 characters");

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative");
    }
}