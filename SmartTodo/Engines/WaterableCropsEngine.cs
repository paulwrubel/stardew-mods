using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class WaterableCropsEngine(Action<ITodoItem>? addToCompletedCache = null) : IEngine
    {
        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if there are harvestable crops in various locations
            foreach (GameLocation gameLocation in GameHelper.GetLocationsAndPhrases())
            {
                if (gameLocation != null)
                {
                    int waterableCount = gameLocation.getTotalUnwateredCrops();
                    if (waterableCount > 0)
                    {
                        items.Add(new WaterableCropsTodoItem(gameLocation, addToCompletedCache: AddToCompletedCache));
                    }
                }
            }

            return items;
        }
    }
}