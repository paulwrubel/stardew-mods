using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;
using StardewValley.GameData;

namespace AutomaticTodoList.Engines;

internal class PassiveFestivalEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<PassiveFestivalTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        // check if today is a passive (multi-day) event
        bool isTodayAPassiveFestivalDay =
            Utility.TryGetPassiveFestivalDataForDay(
                Game1.dayOfMonth, Game1.season, null,
                out string? festivalID,
                out PassiveFestivalData? data
            );

        if (!isTodayAPassiveFestivalDay || festivalID is null || data is null)
        {
            return;
        }

        items.Add(new PassiveFestivalTodoItem(festivalID, data));
    }
}
