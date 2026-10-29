namespace CustomerPortal.Domain;

public class Customer(int id, string surname, string forename, decimal discount, string address)
{
    public Customer(int id, string surname, string forename, string address)
        : this(id, surname, forename, 0m, address) { }

    public int Id { get; } = id;
    public string Surname
    {
        get;
        set => field = Validate(value, nameof(Surname));
    } = Validate(surname, nameof(Surname));

    public string Forename
    {
        get;
        set => field = Validate(value, nameof(Forename));
    } = Validate(forename, nameof(Forename));

    public decimal Discount { get; private set; } = Math.Max(0, discount);

    public string Address
    {
        get;
        set => field = Validate(value, nameof(Address));
    } = Validate(address, nameof(Address));

    private static string Validate(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Invalid value {parameterName}");
        }
        return value.Trim();
    }
}
