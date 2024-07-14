using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A HarvestableCropsTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="HarvestableCropsTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class HarvestableCropsTodoItem(GameLocation location, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.HarvestableCrops)
{
    internal readonly GameLocation Location = location;

    private int RemainingHarvestCount { get; set; } = location.getTotalCropsReadyForHarvest();

    public override string Text()
    {
        return I18n.Items_HarvestableCrops_Text(
            this.Location.GetDisplayName() ?? this.Location.Name,
            this.RemainingHarvestCount
        );
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (!IsChecked)
        {
            var unharvestedCount = this.Location.getTotalCropsReadyForHarvest();
            if (unharvestedCount != this.RemainingHarvestCount)
            {
                this.RemainingHarvestCount = unharvestedCount;

                if (this.RemainingHarvestCount == 0)
                {
                    this.MarkCompleted();
                }
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is HarvestableCropsTodoItem otherItem && this.Location.Name == otherItem.Location.Name;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.Location.Name).GetHashCode();
    }
}
