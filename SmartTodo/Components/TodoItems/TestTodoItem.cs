using SmartTodo.Models;

namespace SmartTodo.Components.TodoItems
{
    /// <summary>A test todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="TestTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class TestTodoItem(string text, bool isChecked = false, Action<ITodoItem>? addToCompletedCache = null)
        : BaseTodoItem(text, isChecked, 0, addToCompletedCache)
    {
        public override bool Equals(object? obj)
        {
            return obj is TestTodoItem otherItem && this.Text == otherItem.Text;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.Text).GetHashCode();
        }
    }
}