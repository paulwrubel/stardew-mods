using Microsoft.Xna.Framework;
using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines
{
    internal class ReadyMachinesEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<ReadyMachinesTodoItem>(log, isEnabled, Frequency.EveryTimeChange)
    {

        public override void UpdateItems()
        {
            // check if there are harvestable machine in various locations
            Utility.ForEachLocation((gameLocation) =>
            {
                if (gameLocation is null)
                {
                    return true;
                }

                int harvestableCount = gameLocation.GetNumberOfReadyMachinesExcludingBuildings();
                if (harvestableCount > 0)
                {
                    items.Add(new ReadyMachinesTodoItem(gameLocation));
                }

                return true;
            });
        }
    }
}