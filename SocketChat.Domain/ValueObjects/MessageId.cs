namespace SocketChat.Domain.ValueObjects
{
    public class MessageId
    {
        public Guid Value { get; }

        private MessageId(Guid value)
        {
            Value = value;
        }

        public static MessageId Create() => new(Guid.NewGuid());

        public static MessageId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MessageId cannot be empty.", nameof(value));

            return new MessageId(value);
        }
    }
}
