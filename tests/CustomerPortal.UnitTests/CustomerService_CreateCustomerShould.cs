using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Validators;

namespace CustomerPortal.UnitTests;

public class CustomerService_CreateCustomerShould
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData("  ", "   ", "   ")]
    [InlineData("John", "", "")]
    [InlineData("John", "Smith", " ")]
    public void CreateCustomer_SurnameForenameAddress_EmptyOrWhiteSpaces_ReturnsFalse(
        string surname,
        string forename,
        string address
    )
    {
        // Assign
        var request = new CreateCustomerRequest(surname, forename, address);
        var validator = new CreateCustomerRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateCustomer_SurnameForenameAddressNotEmptyStringOrWhiteSpace_ReturnsTrue()
    {
        var request = new CreateCustomerRequest("Bond", "James", "Top Secret!");
        var validator = new CreateCustomerRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
}
