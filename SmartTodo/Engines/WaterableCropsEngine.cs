using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class WaterableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<WaterableCropsTodoItem>(log, isEnabled, UpdateFrequency.EverySecond)
    {
        public override void UpdateItems()
        {
            // check if there are waterable crops in various locations
            Utility.ForEachLocation((gameLocation) =>
            {
                if (gameLocation is null)
                {
                    return true;
                }

                int waterableCount = gameLocation.getTotalUnwateredCrops();
                if (waterableCount > 0)
                {
                    items.Add(new WaterableCropsTodoItem(gameLocation));
                }

                return true;
            });
        }
    }
}