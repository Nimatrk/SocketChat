namespace SocketChat.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("Email cannot be null or empty.", nameof(value));

        value = value.Trim().ToLowerInvariant();

        if (value.Length > 254)
            throw new ArgumentException("Email is too long.", nameof(value));

        var atIndex = value.IndexOf('@');

        if (atIndex <= 0 ||
            atIndex != value.LastIndexOf('@') ||
            atIndex == value.Length - 1)
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        return new Email(value);
    }

    public override string ToString() => Value;
}