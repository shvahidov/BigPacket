namespace Domain.Entities;

public class User
{
    public Guid UserId { get; set; }

    public required string UserName { get; set; }

    public required string LoginName { get; set; }

    public required string Password { get; set; }

    public Guid RoleId { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Info { get; set; }

    public Guid? ParentId { get; set; }

    public required Role? Role { get; set; }
}