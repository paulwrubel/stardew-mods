using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class WaterableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<WaterableCropsTodoItem>(log, isEnabled)
    {
        public override void UpdateItems()
        {
            // check if there are harvestable crops in various locations
            foreach (GameLocation gameLocation in GameHelper.GetLocations())
            {
                if (gameLocation is null)
                {
                    continue;
                }

                if (items.Any(item => item.Location.Name == gameLocation.Name))
                {
                    continue;
                }

                int waterableCount = gameLocation.getTotalUnwateredCrops();
                if (waterableCount > 0)
                {
                    items.Add(new WaterableCropsTodoItem(gameLocation));
                }
            }
        }
    }
}