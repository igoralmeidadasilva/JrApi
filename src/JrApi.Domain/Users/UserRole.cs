namespace JrApi.Domain.Users;

[Flags]
public enum UserRole
{
    SuperAdmin = 0,
    Admin = 1,
    Manager = 2,
    Standard = 4,
    None = 8
}