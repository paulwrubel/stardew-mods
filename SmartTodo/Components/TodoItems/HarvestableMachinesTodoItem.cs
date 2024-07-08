using SmartTodo.Models;
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
            bool isChecked = false,
            Action<ITodoItem>? addToCompletedCache = null,
            Func<ITodoItem, bool>? removeFromCompletedCache = null
        ) : base("", isChecked, 10, addToCompletedCache, removeFromCompletedCache)
        {
            this.Location = location;
            this.RemainingMachinesCount = location.getNumberOfMachinesReadyForHarvest();

            this.UpdateText();
        }

        public override void OnTimeChanged()
        {
            base.OnTimeChanged();

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

        public override void OnUpdateTicked()
        {
            base.OnUpdateTicked();

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

        private void UpdateText()
        {
            this.Text = $"Harvest machines ({this.Location.GetDisplayName() ?? this.Location.Name}) ({this.RemainingMachinesCount} remaining)";
        }
    }
}