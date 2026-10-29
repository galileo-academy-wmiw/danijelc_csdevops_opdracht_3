using CustomerPortal.Domain;

namespace CustomerPortal.UnitTests.Domain;

public class CustomerTests
{
    private static readonly Dictionary<
        string,
        (Action<Customer, string> Set, Func<Customer, string> Get)
    > _properties = new()
    {
        ["Surname"] = ((c, v) => c.Surname = v, c => c.Surname),
        ["Forename"] = ((c, v) => c.Forename = v, c => c.Forename),
        ["Address"] = ((c, v) => c.Address = v, c => c.Address),
    };

    public static TheoryData<string> CustomerProperties() => [.. _properties.Keys];

    public static TheoryData<string, string?> CustomerPropertyInvalidInputs()
    {
        TheoryData<string, string?> data = [];
        string?[] testValues = ["", "  ", null];

        foreach (var prop in _properties)
        {
            foreach (var value in testValues)
            {
                data.Add(prop.Key, value);
            }
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(CustomerPropertyInvalidInputs))]
    public void Setter_EmptyOrWhiteSpaceValue_ThrowsArgumentException(
        string name,
        string? testValue
    )
    {
        // Arrange
        var customer = new Customer(1, "Bourne", "Jason", "France");

        var ex = Assert.Throws<ArgumentException>(() =>
            // Act
            _properties[name].Set(customer, testValue!)
        );

        // Assert
        Assert.Contains(name, ex.Message);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("  ", "   ", "   ")]
    [InlineData("John", "", "")]
    [InlineData("John", "Smith", " ")]
    public void New_Customer_EmptyOrWhiteSpaceValue_ThrowsArgumentException(
        string surname,
        string forename,
        string address
    )
    {
        Assert.Throws<ArgumentException>(() =>
            // Act
            new Customer(1, surname, forename, address)
        );
    }

    [Theory]
    [MemberData(nameof(CustomerProperties))]
    public void Setter_ValidValue_StoresValue(string name)
    {
        // Arrange
        var customer = new Customer(1, "Bourne", "Jason", "France");

        // Act
        _properties[name].Set(customer, "newValue");

        // Assert
        Assert.Equal("newValue", _properties[name].Get(customer));
    }

    [Theory]
    [MemberData(nameof(CustomerProperties))]
    public void Setter_ValueWithWhitespaces_StoresTrimmedValue(string name)
    {
        // Arrange
        var customer = new Customer(1, "Moore", "Roger", "England");

        // Act
        _properties[name].Set(customer, " newValue ");

        // Assert
        Assert.Equal("newValue", _properties[name].Get(customer));
    }

    [Fact]
    public void New_Customer_NegativeDiscount_StoresZero()
    {
        // Arrange
        var customer = new Customer(1, "Connery", "Sean", -10m, "Scotland");

        // Assert
        Assert.Equal(0, customer.Discount);
    }
}
