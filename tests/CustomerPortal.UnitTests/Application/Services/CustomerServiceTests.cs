using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Services;
using CustomerPortal.Domain;

namespace CustomerPortal.UnitTests.Application.Services;

public class CustomerServiceTest
{
    class MockCustomerRepository(bool success) : ICustomerRepository
    {
        public Customer? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            return 1;
        }

        public bool Save(Customer customer)
        {
            return success;
        }
    }

    [Theory]
    [InlineData(null, "Smith", "Baker Street")]
    [InlineData("John", "", "Baker Street")]
    [InlineData("John", "Smith", " ")]
    [InlineData("", "Smith", "Baker Street")]
    [InlineData("John", null, "Baker Street")]
    [InlineData("John", "Smith", null)]
    public void CreateCustomer_InvalidRequest_ReturnValidationFalied(
        string? surname,
        string? forename,
        string? address
    )
    {
        // Arrange
        var customerService = new CustomerService(new MockCustomerRepository(false));
        var request = new CreateCustomerRequest(surname!, forename!, address!);

        // Act
        var result = customerService.CreateCustomer(request);

        // Assert
        Assert.Equal(Status.ValidationFailed, result.Status);
    }

    [Fact]
    public void CreateCustomer_DuplicateCustomer_ReturnAlreadyExists()
    {
        // Arrange
        var customerService = new CustomerService(new MockCustomerRepository(false));
        var request = new CreateCustomerRequest("Brown", "James", "Funky Town");

        // Act
        var result = customerService.CreateCustomer(request);

        // Assert
        Assert.Equal(Status.AlreadyExists, result.Status);
    }

    [Fact]
    public void CreateCustomer_ValidRequest_ReturnSuccess()
    {
        // Arrange
        var customerService = new CustomerService(new MockCustomerRepository(true));
        var request = new CreateCustomerRequest("Brown", "James", "Funky Town");

        // Act
        var result = customerService.CreateCustomer(request);

        // Assert
        Assert.Equal(Status.Success, result.Status);
    }
}
