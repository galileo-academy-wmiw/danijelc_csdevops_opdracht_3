namespace CustomerPortal.Domain;

public class Customer(int id, string surname, string forename, decimal discount, string address)
{
    public Customer(int id, string surname, string forename, string address)
        : this(id, surname, forename, 0m, address) { }

    public int Id { get; } = id;
    public string Surname
    {
        get;
        set { Validate(value, nameof(Surname)); }
    } = Validate(surname, nameof(surname));

    public string Forename
    {
        get;
        set { Validate(value, nameof(Forename)); }
    } = Validate(forename, nameof(forename));

    public decimal Discount { get; private set; } =
        discount < 0
            ? throw new ArgumentException($"Invalid discount value: {discount}")
            : discount;

    public string Address { get; set; } = Validate(address, nameof(address));

    internal void SetDiscount(decimal discount)
    {
        Discount = Math.Max(0, discount);
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
