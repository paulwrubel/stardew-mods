using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A birthday todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="BirthdayTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class BirthdayTodoItem(NPC npc, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Birthday)
{
    internal NPC NPC { get; } = npc;

    public override string Text()
    {
        return I18n.Items_Birthday_Text(this.NPC.getName());
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (IsChecked)
        {
            return;
        }

        if (Game1.player.friendshipData.TryGetValue(this.NPC.Name, out Friendship friendship) && friendship.GiftsToday > 0)
        {
            this.MarkCompleted();
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is BirthdayTodoItem otherItem && this.NPC.Name == otherItem.NPC.Name;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.NPC.Name).GetHashCode();
    }
}
