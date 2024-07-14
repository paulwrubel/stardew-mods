using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines;

internal class GiftingEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled,
    Func<string> enabledNPCsString
) : BaseEngine<GiftingTodoItem>(log, isEnabled, Frequency.OnceADay)
{

    public override IEnumerable<ITodoItem> Items()
    {
        return this.items.Where(item =>
        {
            return EnabledForNPC(item.NPC.Name);
        });
    }

    public override void UpdateItems()
    {
        // check if we still need to give gifts out for NPCs
        Utility.ForEachCharacter((npc) =>
        {
            if (npc.CanReceiveGifts() && Game1.player.friendshipData.TryGetValue(npc.Name, out Friendship friendship) && friendship.GiftsThisWeek < 2)
            {
                items.Add(new GiftingTodoItem(npc, friendship));
            }

            return true;
        });
    }

    private bool EnabledForNPC(string npcName)
    {
        bool all = enabledNPCsString().Trim() == "";

        return all || enabledNPCsString().Split(',').Select(s => s.Trim().ToLower()).Contains(npcName.ToLower());
    }
}
