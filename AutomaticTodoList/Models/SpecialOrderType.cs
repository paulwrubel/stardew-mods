namespace AutomaticTodoList.Models;

public enum SpecialOrderType
{
    Standard,
    Qi,
}

public static class SpecialOrderTypeExtensions
{
    public static string ToStardewSpecialOrderTypeString(this SpecialOrderType type)
    {
        return type switch
        {
            SpecialOrderType.Standard => "",
            SpecialOrderType.Qi => "Qi",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
