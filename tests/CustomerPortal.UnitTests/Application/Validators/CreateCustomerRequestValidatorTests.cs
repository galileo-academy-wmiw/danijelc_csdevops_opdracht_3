using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Validators;

namespace CustomerPortal.UnitTests.Application.Validators;

public class CreateCustomerRequestValidatorTests
{
    [Theory]
    [InlineData(null, "Smith", "Baker Street")]
    [InlineData("John", "", "Baker Street")]
    [InlineData("John", "Smith", " ")]
    [InlineData("", "Smith", "Baker Street")]
    [InlineData("John", null, "Baker Street")]
    [InlineData("John", "Smith", null)]
    public void Validate_EmptyOrWhitespaceField_IsNotValid(
        string? surname,
        string? forename,
        string? address
    )
    {
        // Arrange
        var request = new CreateCustomerRequest(surname!, forename!, address!);
        var validator = new CreateCustomerRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_AllFieldsFilled_IsValid()
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
