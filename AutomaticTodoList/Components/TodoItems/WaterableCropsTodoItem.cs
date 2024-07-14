using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A WaterableCropsTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="WaterableCropsTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class WaterableCropsTodoItem(GameLocation location, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.WaterableCrops)
{
    internal readonly GameLocation Location = location;

    private int RemainingUnwateredCount { get; set; } = location.getTotalUnwateredCrops();

    public override string Text()
    {
        return I18n.Items_WaterableCrops_Text(
            this.Location.GetDisplayName() ?? this.Location.Name,
            this.RemainingUnwateredCount
        );
    }

    public override void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e)
    {
        if (!IsChecked)
        {
            var unwateredCount = this.Location.GetTotalUnwateredCropsExcludingGinger();
            if (unwateredCount != this.RemainingUnwateredCount)
            {
                this.RemainingUnwateredCount = unwateredCount;

                if (this.RemainingUnwateredCount == 0)
                {
                    this.MarkCompleted();
                }
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is WaterableCropsTodoItem otherItem && this.Location.Name == otherItem.Location.Name;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.Location.Name).GetHashCode();
    }
}
