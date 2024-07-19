using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData;
using StardewValley.Locations;
using StardewValley.TokenizableStrings;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A passive festival todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="PassiveFestivalTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class PassiveFestivalTodoItem(string id, PassiveFestivalData data, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Birthday)
{
    internal PassiveFestival PassiveFestival = new(id, data);

    public override string Text()
    {
        return PassiveFestival.GetI18nDisplayName();
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (IsChecked)
        {
            return;
        }

        if (Game1.player.IsInPassiveFestivalLocation(PassiveFestival.ID))
        {
            this.MarkCompleted();
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is PassiveFestivalTodoItem otherItem && this.PassiveFestival.Equals(otherItem.PassiveFestival);
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.PassiveFestival).GetHashCode();
    }
}
