using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class BirthdayEngine : IEngine
    {

        public List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if it is anyone's birthday today
            Utility.ForEachCharacter((npc) =>
            {
                if (npc.isBirthday())
                {
                    items.Add(new BirthdayTodoItem(npc));
                }

                return true;
            });

            return items;
        }
    }
}