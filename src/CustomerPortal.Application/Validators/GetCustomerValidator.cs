using CustomerPortal.Domain;
using FluentValidation;

namespace CustomerPortal.Application.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator() { }
}
