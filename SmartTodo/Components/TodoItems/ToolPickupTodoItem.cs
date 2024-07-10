using SmartTodo.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A ToolPickupTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="ToolPickupTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class ToolPickupTodoItem : BaseTodoItem
    {
        private Item ReadyTool { get; set; }

        public ToolPickupTodoItem(Tool tool, bool isChecked = false)
            : base("", isChecked, 90)
        {
            this.ReadyTool = tool;
            this.Text = $"Pick up {tool.DisplayName} from Clint";
        }

        public override void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            if (!IsChecked)
            {
                if (Game1.player.toolBeingUpgraded.Value is null)
                {
                    this.MarkCompleted();
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is ToolPickupTodoItem otherItem && this.ReadyTool.Name == otherItem.ReadyTool.Name;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.ReadyTool.Name).GetHashCode();
        }
    }
}