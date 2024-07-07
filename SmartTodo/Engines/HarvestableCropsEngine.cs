using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableCropsEngine(Action<ITodoItem>? addToCompletedCache = null) : IEngine
    {
        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if there are harvestable crops in various locations
            foreach ((string location, string locationPhrase) in GameHelper.GetLocationsAndPhrases())
            {
                GameLocation gameLocation = Game1.getLocationFromName(location);
                if (gameLocation != null)
                {
                    int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
                    if (harvestableCount > 0)
                    {
                        items.Add(new HarvestableCropsTodoItem(gameLocation, locationPhrase, addToCompletedCache: AddToCompletedCache));
                    }
                }
            }

            return items;
        }
    }
}