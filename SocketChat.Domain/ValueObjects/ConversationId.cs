namespace SocketChat.Domain.ValueObjects
{
    public sealed class ConversationId
    {
        public Guid Value { get; }

        private ConversationId(Guid value)
        {
            Value = value;
        }

        public static ConversationId Create() => new(Guid.NewGuid());

        public static ConversationId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ConversationId cannot be empty.", nameof(value));

            return new ConversationId(value);
        }
    }
}
