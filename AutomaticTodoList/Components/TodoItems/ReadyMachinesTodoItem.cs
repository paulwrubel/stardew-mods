using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A ReadyMachinesTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="ReadyMachinesTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class ReadyMachinesTodoItem(GameLocation location, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.ReadyMachines)
{
    public readonly GameLocation Location = location;

    private int ReadyMachinesCount { get; set; } = location.GetNumberOfReadyMachinesExcludingBuildings();

    public override string Text()
    {
        return I18n.Items_ReadyMachines_Text(
            this.Location.GetDisplayName() ?? this.Location.Name,
            this.ReadyMachinesCount
        );
    }

    public override void OnTimeChanged(TimeChangedEventArgs e)
    {
        if (IsChecked)
        {
            var machineCount = this.Location.GetNumberOfReadyMachinesExcludingBuildings();
            if (machineCount != this.ReadyMachinesCount)
            {
                this.ReadyMachinesCount = machineCount;

                if (this.ReadyMachinesCount > 0)
                {
                    this.MarkUncompleted();
                }
            }
        }
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (!IsChecked)
        {
            var machineCount = this.Location.getNumberOfMachinesReadyForHarvest();
            if (machineCount != this.ReadyMachinesCount)
            {
                this.ReadyMachinesCount = machineCount;

                if (this.ReadyMachinesCount == 0)
                {
                    this.MarkCompleted();
                }
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is ReadyMachinesTodoItem otherItem && this.Location.Name == otherItem.Location.Name;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.Location.Name).GetHashCode();
    }
}
