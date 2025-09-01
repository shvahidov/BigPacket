namespace Domain.Entities;

public class Month
{
    public Guid MonthId { get; set; }

    public required string MonthName { get; set; }
}