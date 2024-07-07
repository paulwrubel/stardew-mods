using StardewValley;

namespace SmartTodo
{
    internal static class GameHelper
    {

        public static IList<GameLocation> GetLocations()
        {
            return Game1.locations;
        }
    }
}