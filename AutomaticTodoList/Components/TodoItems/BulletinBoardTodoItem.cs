using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems
{
    /// <summary>A BulletinBoardTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="BulletinBoardTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class BulletinBoardTodoItem(
        bool isChecked = false
    ) : BaseTodoItem(
        "Check the daily quest bulletin board",
        isChecked,
        TaskPriority.BulletinBoard
    )
    {
        public override void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            if (!IsChecked)
            {
                if (Game1.player.acceptedDailyQuest.Value)
                {
                    this.MarkCompleted();
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is BulletinBoardTodoItem;
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
    }
}