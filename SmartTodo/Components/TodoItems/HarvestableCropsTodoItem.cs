using StardewValley;
using xTile.Dimensions;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A HarvestableCropsTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="HarvestableCropsTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class HarvestableCropsTodoItem : BaseTodoItem
    {
        private readonly GameLocation Location;

        private readonly string LocationPhrase;

        private int RemainingHarvestCount { get; set; }

        public HarvestableCropsTodoItem(GameLocation location, string locationPhrase, bool isChecked = false) : base("", isChecked)
        {
            this.Location = location;
            this.LocationPhrase = locationPhrase;
            this.RemainingHarvestCount = location.getTotalCropsReadyForHarvest();

            this.UpdateText();
        }

        public override void OnUpdateTicked()
        {
            if (!IsChecked)
            {
                if (this.Location.getTotalCropsReadyForHarvest() != this.RemainingHarvestCount)
                {
                    this.RemainingHarvestCount = this.Location.getTotalCropsReadyForHarvest();

                    this.UpdateText();

                    if (this.RemainingHarvestCount == 0)
                    {
                        this.IsChecked = true;
                    }
                }
            }
        }

        private void UpdateText()
        {
            this.Text = $"Harvest crops {this.LocationPhrase} ({this.RemainingHarvestCount} remaining)";
        }
    }
}