using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidation : AbstractValidator<RequestExpenseJson>
{
    public RegisterExpenseValidation()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceErrorMessages.TitleRequired);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AmountGreaterThanZero);
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.ExpenseDateValid);
        RuleFor(x => x.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PaymentTypeInvalid);
    }
}
