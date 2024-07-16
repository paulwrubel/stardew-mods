using System.ComponentModel;
using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A gifting todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="GiftingTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class GiftingTodoItem(NPC npc, Friendship npcFriendship, WeeklyGiftOrdinal ordinal, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Gifting)
{
    internal NPC NPC { get; } = npc;

    internal Friendship NPCFriendship { get; } = npcFriendship;

    internal WeeklyGiftOrdinal Ordinal { get; } = ordinal;

    public override string Text()
    {
        return Ordinal switch
        {
            WeeklyGiftOrdinal.First => I18n.Items_Gifting_FirstGift_Text(this.NPC.getName()),
            WeeklyGiftOrdinal.Second => I18n.Items_Gifting_SecondGift_Text(this.NPC.getName()),
            _ => throw new InvalidEnumArgumentException(nameof(Ordinal), (int)Ordinal, typeof(WeeklyGiftOrdinal)),
        };
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
        return obj is GiftingTodoItem otherItem && this.NPC.Name == otherItem.NPC.Name && this.Ordinal == otherItem.Ordinal;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.NPC.Name, this.Ordinal).GetHashCode();
    }
}
