namespace Domain.Enums;

using Ardalis.SmartEnum;

public sealed class MonthEnum(string name, int value)
    : SmartEnum<MonthEnum, int>(name, value)
{
    public static readonly MonthEnum January = new(nameof(January), 1);
    public static readonly MonthEnum February = new(nameof(February), 2);
    public static readonly MonthEnum March = new(nameof(March), 3);
    public static readonly MonthEnum April = new(nameof(April), 4);
    public static readonly MonthEnum May = new(nameof(May), 5);
    public static readonly MonthEnum June = new(nameof(June), 6);
    public static readonly MonthEnum July = new(nameof(July), 7);
    public static readonly MonthEnum August = new(nameof(August), 8);
    public static readonly MonthEnum September = new(nameof(September), 9);
    public static readonly MonthEnum October = new(nameof(October), 10);
    public static readonly MonthEnum November = new(nameof(November), 11);
    public static readonly MonthEnum December = new(nameof(December), 12);
}