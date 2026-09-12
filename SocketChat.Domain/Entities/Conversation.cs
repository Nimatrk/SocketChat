using SocketChat.Domain.Enums;
using SocketChat.Domain.ValueObjects;

namespace SocketChat.Domain.Entities
{
    public sealed class Conversation
    {
        public ConversationId Id { get; private set; }
        public ConversationType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Conversation(ConversationType type)
        {
            Id = ConversationId.Create();
            Type = type;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public static Conversation CreatePrivate()
        {
            return new(ConversationType.Private);
        }

        public static Conversation CreateGroup()
        {
            return new(ConversationType.Group);
        }

        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}