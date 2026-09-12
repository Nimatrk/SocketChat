namespace SocketChat.Domain.ValueObjects
{
    public sealed record UserId
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Create() => new UserId(Guid.NewGuid());
    }
}