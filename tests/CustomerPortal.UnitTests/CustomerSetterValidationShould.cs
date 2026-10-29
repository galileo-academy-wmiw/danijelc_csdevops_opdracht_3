using CustomerPortal.Domain;

namespace CustomerPortal.UnitTests;

public class CustomerSetterValidationShould
{
    public static TheoryData<
        string,
        Action<Customer, string>,
        Func<Customer, string>,
        string
    > CustomerPropertyTests =>
        new()
        {
            { "Surname", (c, v) => c.Surname = v, c => c.Surname, null! },
            { "Forename", (c, v) => c.Forename = v, c => c.Forename, "" },
            { "Address", (c, v) => c.Address = v, c => c.Address, "   " },
        };

    [Theory]
    [MemberData(nameof(CustomerPropertyTests), DisableDiscoveryEnumeration = true)]
    public void Throw_WhenValueIsEmpty(
        string propertyName,
        Action<Customer, string> testProperty,
        Func<Customer, string> _,
        string testValue
    )
    {
        //Arrange
        var customer = new Customer(1, "Bourne", "Jason", "France") { };

        var ex = Assert.Throws<ArgumentException>(() =>
            //Act
            testProperty(customer, testValue)
        );

        // Assert
        Assert.Contains(propertyName, ex.Message);
    }

    [Theory]
    [MemberData(nameof(CustomerPropertyTests), DisableDiscoveryEnumeration = true)]
    public void Check_IfValidValuesAssignedToPropertiesAreSaved(
        string _1,
        Action<Customer, string> testProperty,
        Func<Customer, string> getProperty,
        string _2
    )
    {
        // Arrange
        var customer = new Customer(1, "Bourne", "Jason", "France") { };

        // Act
        testProperty(customer, "newValue");

        // Assert
        Assert.Equal("newValue", getProperty(customer));
    }
}
