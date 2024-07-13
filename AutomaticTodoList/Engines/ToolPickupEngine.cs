using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines;

internal class ToolPickupEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<ToolPickupTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        // check if a tool is ready
        if (
            Game1.player.toolBeingUpgraded.Value is not null && // check if there's a tool that we're upgrading right now...
            (int)Game1.player.daysLeftForToolUpgrade.Value <= 0 && // ...and if that tool is ready
            !Utility.isFestivalDay() && // we can't pick up tools during festivals
                (!Game1.player.hasCompletedCommunityCenter() ||                     // if the player hasn't completed the CC, then Clint's schedule is normal...
                Game1.shortDayNameFromDayOfSeason(Game1.dayOfMonth) != "Fri" ||     // ...otherwise, Clint doesn't work on fridays...
                Game1.isRaining)                                                    // ...unless it is raining
        )
        {
            items.Add(new ToolPickupTodoItem(Game1.player.toolBeingUpgraded.Value));
        }
    }
}
