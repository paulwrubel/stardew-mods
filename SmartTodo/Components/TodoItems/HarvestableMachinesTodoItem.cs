using SmartTodo.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A HarvestableMachinesTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="HarvestableMachinesTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class HarvestableMachinesTodoItem : BaseTodoItem
    {
        public readonly GameLocation Location;

        private int RemainingMachinesCount { get; set; }

        public HarvestableMachinesTodoItem(
            GameLocation location,
            bool isChecked = false
        ) : base("", isChecked, 30)
        {
            this.Location = location;
            this.RemainingMachinesCount = location.getNumberOfMachinesReadyForHarvest();

            this.UpdateText();
        }

        public override void OnTimeChanged(TimeChangedEventArgs e)
        {
            if (IsChecked)
            {
                var machineCount = this.Location.getNumberOfMachinesReadyForHarvest();
                if (machineCount != this.RemainingMachinesCount)
                {
                    this.RemainingMachinesCount = machineCount;

                    this.UpdateText();

                    if (this.RemainingMachinesCount > 0)
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
                if (machineCount != this.RemainingMachinesCount)
                {
                    this.RemainingMachinesCount = machineCount;

                    this.UpdateText();

                    if (this.RemainingMachinesCount == 0)
                    {
                        this.MarkCompleted();
                    }
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is HarvestableMachinesTodoItem otherItem && this.Location.Name == otherItem.Location.Name;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.Location.Name).GetHashCode();
        }

        private void UpdateText()
        {
            this.Text = $"Harvest machines ({this.Location.GetDisplayName() ?? this.Location.Name}) ({this.RemainingMachinesCount} remaining)";
        }


    }
}