using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A birthday todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="BirthdayTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class BirthdayTodoItem : BaseTodoItem
    {
        private NPC NPC { get; set; }

        public BirthdayTodoItem(NPC npc, bool isChecked = false, Action<ITodoItem>? addToCompletedCache = null)
            : base("", isChecked, 100, addToCompletedCache)
        {
            this.NPC = npc;
            this.Text = $"Give {npc.getName()} a birthday gift";
        }

        public override void OnUpdateTicked()
        {
            if (!IsChecked)
            {
                if (Game1.player.friendshipData[this.NPC.Name].GiftsToday > 0)
                {
                    this.MarkCompleted();
                }
            }
        }
    }
}