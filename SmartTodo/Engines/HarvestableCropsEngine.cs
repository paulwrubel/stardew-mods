using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableCropsEngine : IEngine
    {

        private static readonly (string, string)[] Locations = [
            ("Farm", "on the Farm"),
            ("FarmHouse", "in the Farmhouse"),
            ("Greenhouse", "in the Greenhouse"),
            ("Town", "in Town"),
            ("Beach", "on the Beach"),
            ("Mountain", "at the Mountains"),
            ("Forest", "in Cindersap Forest"), // cindersap forest
            ("Desert", "in the Desert"),
            ("Woods", "in the Secret Woods"), // secret woods
            ("Backwoods", "in the Backwoods"), // farm <--> mountains
            ("IslandWest", "on the Ginger Island Farm"), // the farm region of Ginger Island
            ("IslandFarmHouse", "in the Ginger Island Farmhouse")
        ];

        public List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if there are harvestable crops in various locations
            foreach ((string location, string locationPhrase) in Locations)
            {
                GameLocation gameLocation = Game1.getLocationFromName(location);
                if (gameLocation != null)
                {
                    int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
                    if (harvestableCount > 0)
                    {
                        items.Add(new HarvestableCropsTodoItem(gameLocation, locationPhrase));
                    }
                }
            }

            return items;
        }
    }
}