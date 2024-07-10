using SmartTodo.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A WaterableCropsTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="WaterableCropsTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class WaterableCropsTodoItem : BaseTodoItem
    {
        internal readonly GameLocation Location;

        private int RemainingUnwateredCount { get; set; }

        public WaterableCropsTodoItem(GameLocation location, bool isChecked = false)
            : base("", isChecked, 19)
        {
            this.Location = location;
            this.RemainingUnwateredCount = location.getTotalUnwateredCrops();

            this.UpdateText();
        }

        public override void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e)
        {
            if (!IsChecked)
            {
                var unwateredCount = this.Location.GetTotalUnwateredCropsExcludingGinger();
                if (unwateredCount != this.RemainingUnwateredCount)
                {
                    this.RemainingUnwateredCount = unwateredCount;

                    this.UpdateText();

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

        private void UpdateText()
        {
            this.Text = $"Water crops ({this.Location.GetDisplayName() ?? this.Location.Name}) ({this.RemainingUnwateredCount} remaining)";
        }
    }
}