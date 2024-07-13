using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines;

internal class BulletinBoardEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<BulletinBoardTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        // check if any bulletin board has new content
        if (Game1.CanAcceptDailyQuest())
        {
            items.Add(new BulletinBoardTodoItem());
        }
    }
}
