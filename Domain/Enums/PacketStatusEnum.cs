namespace Domain.Enums;

using Ardalis.SmartEnum;

public sealed class PacketStatusEnum(string name, int value)
    : SmartEnum<PacketStatusEnum, int>(name, value)
{
    public static readonly PacketStatusEnum Active = new(nameof(Active), 1);
    public static readonly PacketStatusEnum Expired = new(nameof(Expired), 2);
    public static readonly PacketStatusEnum Pending = new(nameof(Pending), 3);
    public static readonly PacketStatusEnum Disabled = new(nameof(Disabled), 4);
}