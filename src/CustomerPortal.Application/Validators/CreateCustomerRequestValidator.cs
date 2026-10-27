using CustomerPortal.Application.Requests;
using FluentValidation;

namespace CustomerPortal.Application.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(request => request.Surname).NotNull().NotEmpty();
        RuleFor(request => request.Forename).NotNull().NotEmpty();
        RuleFor(request => request.Address).NotNull().NotEmpty();
    }
}
