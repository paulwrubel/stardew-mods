using StardewValley;

namespace SmartTodo
{
    public enum SpecialOrderType
    {
        Standard,
        Qi,
    }

    internal static class GameHelper
    {

        public static IList<GameLocation> GetLocations()
        {
            return Game1.locations;
        }

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