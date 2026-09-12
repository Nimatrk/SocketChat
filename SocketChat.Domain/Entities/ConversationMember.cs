using SocketChat.Domain.ValueObjects;

namespace SocketChat.Domain.Entities
{
    public sealed class ConversationMember
    {
        public ConversationId ConversationId { get; private set; }
        public UserId UserId { get; private set; }
        public DateTime JoinedAt { get; private set; }

        private ConversationMember(ConversationId conversationId, UserId userId)
        {
            ConversationId = conversationId;
            UserId = userId;
            JoinedAt = DateTime.UtcNow;
        }

        public static ConversationMember Create(ConversationId conversationId, UserId userId)
        {
            return new(conversationId, userId);
        }
    }
}
