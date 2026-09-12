using SocketChat.Domain.Enums;
using SocketChat.Domain.ValueObjects;

namespace SocketChat.Domain.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserStatus Status { get; private set; }
    public AccountStatus AccountStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime LastSeenAt { get; private set; }

    private User(string firstName, string? lastName, Username username, Email email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

        var now = DateTime.UtcNow;

        Id = UserId.Create();
        FirstName = firstName.Trim();
        LastName = string.IsNullOrWhiteSpace(lastName) ? null : lastName.Trim();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Status = UserStatus.Offline;
        AccountStatus = AccountStatus.Active;
        CreatedAt = now;
        UpdatedAt = now;
        LastSeenAt = now;
    }

    public static User Create(string firstName, string? lastName, Username username, Email email, string passwordHash)
    {
        return new User(firstName, lastName, username, email, passwordHash);
    }

    public void SetOnline()
    {
        Status = UserStatus.Online;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAway()
    {
        Status = UserStatus.Away;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetOffline()
    {
        var now = DateTime.UtcNow;
        Status = UserStatus.Offline;
        UpdatedAt = now;
        LastSeenAt = now;
    }

    public void Lock()
    {
        AccountStatus = AccountStatus.Locked;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unlock()
    {
        AccountStatus = AccountStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Ban()
    {
        AccountStatus = AccountStatus.Banned;
        UpdatedAt = DateTime.UtcNow;
    }
}