using AutomaticTodoList.Models;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A test todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="TestTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class TestTodoItem(string text, bool isChecked = false)
    : BaseTodoItem(text, isChecked, TaskPriority.Test)
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
