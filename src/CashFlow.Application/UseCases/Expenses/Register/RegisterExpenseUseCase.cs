using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);

        return new ResponseExpenseJson();
    }

    private void Validate(RequestExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if(titleIsEmpty)
        {
            throw new ArgumentException("The title is required.");
        }

        if(request.Amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than zero.");
        }

        var result = DateTime.Compare(DateTime.UtcNow, request.Date);
        if (result < 0)
        {
            throw new ArgumentException("Expenses cannot be for the future.");
        }

        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if(!paymentTypeIsValid)
        {
            throw new ArgumentException("Payment type is not valid.");
        }
    } 
}
