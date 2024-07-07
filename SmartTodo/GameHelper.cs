using StardewValley;

namespace SmartTodo
{
    internal static class GameHelper
    {
        public static IList<GameLocation> GetLocationsAndPhrases()
        {

            // List<(string, string)> locations = [
            //     ("Farm", "on the Farm"),
            //     ("FarmHouse", "in the Farmhouse"),
            //     ("Greenhouse", "in the Greenhouse"),
            //     ("Town", "in Town"),
            //     ("Beach", "on the Beach"),
            //     ("Mountain", "at the Mountains"),
            //     ("Forest", "in Cindersap Forest"), // cindersap forest
            //     ("Desert", "in the Desert"),
            //     ("Woods", "in the Secret Woods"), // secret woods
            //     ("Backwoods", "in the Backwoods"), // farm <--> mountains
            //     ("IslandWest", "on the Ginger Island Farm"), // the farm region of Ginger Island
            //     ("IslandFarmHouse", "in the Ginger Island Farmhouse")
            // ];

            // foreach (GameLocation location in Game1.locations)
            // {!locations.Any(loc => loc.Item1 == location.Name))
            //     {
            //         locations.Add((location.Name, $"({location.Name})"));
            //     }
            // }

            return Game1.locations;
        }
    }
}