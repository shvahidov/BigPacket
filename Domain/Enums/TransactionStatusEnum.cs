namespace Domain.Enums;

using Ardalis.SmartEnum;

public sealed class TransactionStatusEnum(string name, int value)
    : SmartEnum<TransactionStatusEnum, int>(name, value)
{
    public static readonly TransactionStatusEnum Pending = new(nameof(Pending), 1);
    public static readonly TransactionStatusEnum Completed = new(nameof(Completed), 2);
    public static readonly TransactionStatusEnum Failed = new(nameof(Failed), 3);
    public static readonly TransactionStatusEnum RolledBack = new(nameof(RolledBack), 4);
}