using CustomerPortal.Application.Requests;
using FluentValidation;

namespace CustomerPortal.Application.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(request => request.Surname.Trim()).NotEmpty();
        RuleFor(request => request.Forename.Trim()).NotEmpty();
        RuleFor(request => request.Address.Trim()).NotEmpty();
    }
}
