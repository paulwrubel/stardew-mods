using StardewValley;

namespace AutomaticTodoList;

internal static class GameHelper
{
    public static IEnumerator<GameLocation> LocationsEnumerator()
    {
        // copy the list in case the reference changes between enumerator steps
        var locations = new List<GameLocation>(Game1.locations);

        foreach (GameLocation location in locations)
        {
            yield return location;
        }
    }
}
