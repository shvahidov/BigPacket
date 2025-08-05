namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }

    public string? OwnerName { get; set; }

    public decimal Balance { get; set; }
}