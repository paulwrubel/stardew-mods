using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;

namespace AutomaticTodoList.Engines
{
    internal class BirthdayEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<BirthdayTodoItem>(log, isEnabled, Frequency.OnceADay)
    {

        public override void UpdateItems()
        {
            // check if it is anyone's birthday today
            Utility.ForEachCharacter((npc) =>
            {
                if (!npc.isBirthday())
                {
                    return true;
                }

                // check if we already made an item for this npc
                if (items.Any(item => item.NPC.Name == npc.Name))
                {
                    return true;
                }

                items.Add(new BirthdayTodoItem(npc));

                return true;
            });
        }
    }
}