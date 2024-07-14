using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A gifting todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="GiftingTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class GiftingTodoItem(NPC npc, Friendship npcFriendship, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Gifting)
{
    internal NPC NPC { get; } = npc;

    internal Friendship NPCFriendship { get; } = npcFriendship;

    public override string Text()
    {
        return I18n.Items_Gifting_Text(this.NPC.getName(), NPCFriendship.GiftsThisWeek);
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
        return obj is GiftingTodoItem otherItem && this.NPC.Name == otherItem.NPC.Name;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.NPC.Name).GetHashCode();
    }
}
