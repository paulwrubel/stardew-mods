using StardewValley;

namespace SmartTodo
{
    public enum UpdateFrequency
    {
        OnceADay,
        EveryTimeChange,
        EverySecond,
        EveryTick,
    }

    public enum SpecialOrderType
    {
        Standard,
        Qi,
    }

    internal static class GameHelper
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
}