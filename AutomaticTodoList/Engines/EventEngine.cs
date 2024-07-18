// using AutomaticTodoList.Components.TodoItems;
// using AutomaticTodoList.Models;
// using StardewValley;
// using StardewValley.GameData;

// namespace AutomaticTodoList.Engines;

// internal class EventEngine(
//     Action<string, StardewModdingAPI.LogLevel> log,
//     Func<bool> isEnabled
// ) : BaseEngine<EventTodoItem>(log, isEnabled, Frequency.OnceADay)
// {
//     public override void UpdateItems()
//     {
//         if (!Utility.isFestivalDay() || !Utility.IsPassiveFestivalDay())
//         {
//             // escape early if there's definitely not a festival today
//             return;
//         }

//         // check if today is a single-day event
//         string festivalKey = $"{Game1.currentSeason}{Game1.dayOfMonth}";
//         var festivalDates = DataLoader.Festivals_FestivalDates(Game1.temporaryContent);

//         if (festivalDates.TryGetValue(festivalKey, out string? festivalName) && festivalName is not null)
//         {
//             items.Add(new EventTodoItem(festivalName, false));
//             return;
//         }

//         // check if today is a passive (multi-day) event
//         if (Utility.TryGetPassiveFestivalDataForDay(
//             Game1.dayOfMonth, Game1.season, null,
//             out string? festivalID,
//             out PassiveFestivalData? data) &&
//             festivalID is not null &&
//             data is not null
//         )
//         {
//             items.Add(new EventTodoItem(festivalID, true));
//         }
//     }
// }
