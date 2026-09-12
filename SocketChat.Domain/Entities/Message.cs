using SocketChat.Domain.Enums;
using SocketChat.Domain.ValueObjects;

namespace SocketChat.Domain.Entities
{
    public sealed class Message
    {
        public MessageId Id { get; private set; }

        public ConversationId ConversationId { get; private set; }

        public UserId SenderId { get; private set; }

        public string Content { get; private set; }

        public MessageType Type { get; private set; }

        public MessageStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? EditedAt { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        private Message(ConversationId conversationId, UserId senderId, string content, MessageType type)
        {
            Id = MessageId.Create();
            ConversationId = conversationId;
            SenderId = senderId;
            Content = content;
            Type = type;
            Status = MessageStatus.Created;
            CreatedAt = DateTime.UtcNow;
            EditedAt = null;
            DeletedAt = null;
        }

        public static Message Create(ConversationId conversationId, UserId senderId, string content, MessageType type = MessageType.Text)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Message content cannot be empty.", nameof(content));

            return new(conversationId, senderId, content, type);
        }

        public void Edit(string newContent)
        {
            if (Status == MessageStatus.Deleted)
                throw new InvalidOperationException("Cannot edit a deleted message.");

            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Message content cannot be empty.", nameof(newContent));

            Content = newContent.Trim();
            EditedAt = DateTime.UtcNow;
        }

        public void MarkAsDelivered()
        {
            if (Status == MessageStatus.Sent)
                Status = MessageStatus.Delivered;
        }

        public void MarkAsRead()
        {
            if (Status is MessageStatus.Sent or MessageStatus.Delivered)
                Status = MessageStatus.Read;
        }

        public void Delete()
        {
            if (Status == MessageStatus.Deleted)
                return;

            Status = MessageStatus.Deleted;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
