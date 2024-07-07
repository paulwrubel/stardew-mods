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
            foreach (GameLocation gameLocation in GameHelper.GetLocations())
            {
                if (gameLocation != null)
                {
                    int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
                    if (harvestableCount > 0)
                    {
                        items.Add(new HarvestableCropsTodoItem(gameLocation, addToCompletedCache: AddToCompletedCache));
                    }
                }
            }

            return items;
        }
    }
}