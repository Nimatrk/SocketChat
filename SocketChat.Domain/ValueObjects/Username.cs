namespace SocketChat.Domain.ValueObjects
{
    public sealed class Username
    {
        public string Value { get; }

        private Username(string value)
        {
            Value = value;
        }

        public static Username Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(value));
            }
            return new(value);
        }
    }
}
