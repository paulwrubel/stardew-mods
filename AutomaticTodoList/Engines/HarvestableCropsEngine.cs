using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines;

internal class HarvestableCropsEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<HarvestableCropsTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        // check if there are harvestable crops in various locations
        //
        // this operation hurts, but it only happens once per day so it's mostly ok
        Utility.ForEachLocation((gameLocation) =>
        {
            if (gameLocation is null)
            {
                return true;
            }

            int harvestableCount = gameLocation.GetTotalCropsReadyForHarvestExcludingForagables();
            if (harvestableCount > 0)
            {
                items.Add(new HarvestableCropsTodoItem(gameLocation));
            }

            return true;
        });
    }
}
