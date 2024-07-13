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
        private IEnumerator<GameLocation>? locations = null;

        public override void UpdateItems()
        {
            locations ??= GameHelper.LocationsEnumerator();

            // only try to check one location each update, for performance reasons
            if (!locations.MoveNext() || locations.Current is null)
            {
                // try again next time!
                locations = null;
                return;
            }

            GameLocation thisLocation = locations.Current;

            this.Log($"Checking waterable crops in {thisLocation.Name}", StardewModdingAPI.LogLevel.Debug);

            int waterableCount = thisLocation.GetTotalUnwateredCropsExcludingGinger();
            if (waterableCount > 0)
            {
                items.Add(new WaterableCropsTodoItem(thisLocation));
            }
        }
    }
}