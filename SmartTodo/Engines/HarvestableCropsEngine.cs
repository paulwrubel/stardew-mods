using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<HarvestableCropsTodoItem>(log, isEnabled, UpdateFrequency.OnceADay)
    {
        public override void UpdateItems()
        {
            // check if there are harvestable crops in various locations
            Utility.ForEachLocation((gameLocation) =>
            {
                if (gameLocation is null)
                {
                    return true;
                }

                int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
                if (harvestableCount > 0)
                {
                    items.Add(new HarvestableCropsTodoItem(gameLocation));
                }

                return true;
            });
        }
    }
}