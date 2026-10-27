using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Validators;

namespace CustomerPortal.UnitTests;

public class CreateCustomerRequestValidatorShould
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData("  ", "   ", "   ")]
    [InlineData("John", "", "")]
    [InlineData("John", "Smith", " ")]
    public void Validator_SurnameForenameAddress_EmptyOrWhiteSpaces_ReturnsFalse(
        string surname,
        string forename,
        string address
    )
    {
        // Arrange
        var request = new CreateCustomerRequest(surname, forename, address);
        var validator = new CreateCustomerRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_SurnameForenameAddressNotEmptyStringOrWhiteSpace_ReturnsTrue()
    {
        // Arrange
        var request = new CreateCustomerRequest("Bond", "James", "Top Secret!");
        var validator = new CreateCustomerRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
}
