using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A BulletinBoardTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="BulletinBoardTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class BulletinBoardTodoItem(bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.BulletinBoard)
{
    public override string Text()
    {
        return I18n.Items_BulletinBoard_Text();
    }

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
