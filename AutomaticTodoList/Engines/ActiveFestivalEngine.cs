using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;
using StardewValley.GameData;

namespace AutomaticTodoList.Engines;

internal class ActiveFestivalEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<ActiveFestivalTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        // check if today is a single-day event
        string festivalKey = $"{Utility.getSeasonKey(Game1.season)}{Game1.dayOfMonth}";
        var festivalDates = DataLoader.Festivals_FestivalDates(Game1.temporaryContent);

        if (!festivalDates.TryGetValue(festivalKey, out string? festivalName))
        {
            // no festival today :(
            return;
        }

        if (!Event.tryToLoadFestivalData(festivalKey, out _, out _, out string locationName, out int startTime, out int endTime))
        {
            // couldn't get festival data for some reason
            return;
        }

        items.Add(new ActiveFestivalTodoItem(festivalKey, festivalName));
    }
}
