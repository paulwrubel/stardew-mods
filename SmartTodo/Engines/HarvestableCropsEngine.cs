using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<HarvestableCropsTodoItem>(log, isEnabled)
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

                // check if we already made an item for this location
                if (items.Any(item => item.Location.Name == gameLocation.Name))
                {
                    continue;
                }

                int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
                if (harvestableCount > 0)
                {
                    items.Add(new HarvestableCropsTodoItem(gameLocation));
                }
            }
        }
    }
}