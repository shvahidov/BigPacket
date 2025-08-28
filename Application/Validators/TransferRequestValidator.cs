namespace Application.Validators;

using Domain.Entities;
using FluentValidation;

public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Сумма перевода должна быть больше 0");

        RuleFor(x => x.FromAccountId)
            .NotEqual(x => x.ToAccountId).WithMessage("Нельзя переводить деньги на тот же самый аккаунт");
    }
}