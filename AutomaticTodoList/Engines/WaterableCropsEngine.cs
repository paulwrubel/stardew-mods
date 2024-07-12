using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines
{
    internal class WaterableCropsEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<WaterableCropsTodoItem>(log, isEnabled, Frequency.EveryTick)
    {

        // get an endless enumerator of all game locations
        private readonly IEnumerator<GameLocation?> locations = GameHelper.EndlessLocationsEnumerator();

        public override void UpdateItems()
        {
            // only try to check one location each update, for performance reasons
            if (!locations.MoveNext() || locations.Current is null)
            {
                // try again next time!
                return;
            }

            GameLocation thisLocation = locations.Current;

            this.Log($"Checking waterable crops in {thisLocation.Name}", StardewModdingAPI.LogLevel.Trace);

            int waterableCount = thisLocation.GetTotalUnwateredCropsExcludingGinger();
            if (waterableCount > 0)
            {
                items.Add(new WaterableCropsTodoItem(thisLocation));
            }
        }
    }
}