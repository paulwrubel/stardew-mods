using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="BaseTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal abstract class BaseTodoItem(
    bool isChecked = false,
    TaskPriority priority = TaskPriority.Default
) : ITodoItem
{
    /// <summary>The checkbox state of the todo item.</summary>
    public bool IsChecked { get; set; } = isChecked;

    /// <summary>The priority of the todo item. A higher number means a higher priority.</summary>
    public TaskPriority Priority { get; set; } = priority;

    public abstract string Text();

    public virtual void MarkCompleted()
    {
        this.IsChecked = true;
    }

    public virtual void MarkUncompleted()
    {
        this.IsChecked = false;
    }

    public virtual void OnDayStarted(DayStartedEventArgs e) { }

    public virtual void OnTimeChanged(TimeChangedEventArgs e) { }

    public virtual void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e) { }

    public virtual void OnUpdateTicked(UpdateTickedEventArgs e) { }

    public virtual void OnMenuChanged(MenuChangedEventArgs e) { }

    public abstract override bool Equals(object? obj);

    public abstract override int GetHashCode();
}
