namespace Domain.Entities;

public class Role
{
    public Guid RoleId { get; set; }

    public required string RoleName { get; set; }
}