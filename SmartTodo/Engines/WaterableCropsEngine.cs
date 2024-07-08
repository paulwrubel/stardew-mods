using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class WaterableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Action<ITodoItem>? addToCompletedCache = null
    ) : BaseEngine(log)
    {
        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public override List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if there are harvestable crops in various locations
            foreach (GameLocation gameLocation in GameHelper.GetLocations())
            {
                if (gameLocation is not null)
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