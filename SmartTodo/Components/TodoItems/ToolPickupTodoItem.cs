using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A ToolPickupTodoItem todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="ToolPickupTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class ToolPickupTodoItem : BaseTodoItem
    {
        private Tool ReadyTool { get; set; }

        public ToolPickupTodoItem(Tool tool, bool isChecked = false, Action<ITodoItem>? addToCompletedCache = null)
            : base("", isChecked, 90, addToCompletedCache)
        {
            this.ReadyTool = tool;
            this.Text = $"Pick up {tool.DisplayName} from Clint";
        }

        public override void OnUpdateTicked()
        {
            if (!IsChecked)
            {
                if (Game1.player.toolBeingUpgraded.Value is null)
                {
                    this.MarkCompleted();
                }
            }
        }
    }
}