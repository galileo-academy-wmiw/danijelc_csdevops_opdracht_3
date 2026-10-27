using CustomerPortal.Application.Requests;
using FluentValidation;

namespace CustomerPortal.Application.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(request => request.Surname).NotEmpty();
        RuleFor(request => request.Forename).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
    }
}
