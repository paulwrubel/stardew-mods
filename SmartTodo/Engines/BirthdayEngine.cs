using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class BirthdayEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Action<ITodoItem>? addToCompletedCache = null
    ) : BaseEngine(log)
    {

        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public override List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if it is anyone's birthday today
            Utility.ForEachCharacter((npc) =>
            {
                if (npc.isBirthday() && Game1.player.friendshipData[npc.Name].GiftsToday == 0)
                {
                    items.Add(new BirthdayTodoItem(npc, addToCompletedCache: AddToCompletedCache));
                }

                return true;
            });

            return items;
        }
    }
}