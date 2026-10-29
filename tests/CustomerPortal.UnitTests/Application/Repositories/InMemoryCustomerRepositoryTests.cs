using CustomerPortal.Application.Repositories;

namespace CustomerPortal.UnitTests.Application.Repositories;

public class InMemoryCustomerRepositoryTests
{
    [Fact]
    public void NextId_FirstCall_ReturnsOne()
    {
        // Arrange
        var repository = new InMemoryCustomerRepository();

        // Act
        var newId = repository.NextId();

        // Assert
        Assert.Equal(1, newId);
    }
}
