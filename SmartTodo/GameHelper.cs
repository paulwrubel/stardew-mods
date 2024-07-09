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
        public static IEnumerator<GameLocation> LocationsEnumerator()
        {
            foreach (GameLocation location in Game1.locations)
            {
                yield return location;
            }
        }

        public static IEnumerator<GameLocation?> EndlessLocationsEnumerator()
        {
            while (true)
            {
                var locations = Game1.locations;
                if (locations.Count == 0)
                {
                    yield return null;
                }
                else
                {
                    foreach (GameLocation location in Game1.locations)
                    {
                        yield return location;
                    }
                }
            }
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