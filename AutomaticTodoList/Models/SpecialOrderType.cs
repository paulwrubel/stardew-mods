using System.ComponentModel;
using StardewValley;
using StardewValley.SpecialOrders;

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

    public static bool IsBoardUnlocked(this SpecialOrderType type)
    {
        return type switch
        {
            SpecialOrderType.Standard => SpecialOrder.IsSpecialOrdersBoardUnlocked(),
            SpecialOrderType.Qi => Math.Max(0, Game1.netWorldState.Value.GoldenWalnutsFound - 1) >= 100,
            _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(SpecialOrderType)),
        };
    }
}
