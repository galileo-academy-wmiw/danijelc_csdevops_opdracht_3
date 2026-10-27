namespace CustomerPortal.Domain;

public class Customer(Guid id, string surname, string forename, decimal discount, string address)
{
    public Customer(string surname, string forename, decimal discount, string address)
        : this(Guid.NewGuid(), surname, forename, discount, address) { }

    public Guid Id { get; } = id;
    public string Surname { get; set; } = Validate(surname, nameof(surname));
    public string Forename { get; set; } = Validate(forename, nameof(forename));
    public decimal Discount { get; private set; } =
        discount < 0
            ? throw new ArgumentException($"Invalid discount value: {discount}")
            : discount;
    public string Address { get; set; } = Validate(address, nameof(address));

    internal void SetDiscount(decimal discount)
    {
        Discount = discount;
    }

    private static string Validate(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Invalid value {parameterName}");
        }
        return value.Trim();
    }
}
