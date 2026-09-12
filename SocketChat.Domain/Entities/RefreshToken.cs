using SocketChat.Domain.ValueObjects;

namespace SocketChat.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public UserId UserId { get; private set; }

        public string TokenHash { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        private RefreshToken(UserId userId, string tokenHash, DateTime expiresAt)
        {
            if (string.IsNullOrWhiteSpace(tokenHash))
                throw new ArgumentException("Token hash cannot be empty.", nameof(tokenHash));
            if (ExpiresAt <= DateTime.UtcNow)
                throw new ArgumentException("Expiration must be in the future.", nameof(expiresAt));

            Id = Guid.NewGuid();
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
            RevokedAt = null;
        }

        public static RefreshToken Create(UserId userId, string tokenHash, DateTime expiresAt)
        {
            return new(userId, tokenHash, expiresAt);
        }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public bool IsRevoked => IsExpired || RevokedAt.HasValue;

        public bool IsActive => !IsExpired && !IsRevoked;

        public void Revoke()
        {
            if (IsRevoked)
                return;

            RevokedAt = DateTime.UtcNow;
        }
    }
}