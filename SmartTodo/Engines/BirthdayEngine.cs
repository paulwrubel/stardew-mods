using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class BirthdayEngine(Action<ITodoItem>? addToCompletedCache = null) : IEngine
    {

        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if it is anyone's birthday today
            Utility.ForEachCharacter((npc) =>
            {
                if (npc.isBirthday())
                {
                    items.Add(new BirthdayTodoItem(npc, addToCompletedCache: AddToCompletedCache));
                }

                return true;
            });

            return items;
        }
    }
}