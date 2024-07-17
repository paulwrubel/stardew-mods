using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A Queen of Sauce todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="QueenOfSauceTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class QueenOfSauceTodoItem(string recipeName, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Birthday)
{
    internal string RecipeName { get; } = recipeName;

    public override string Text()
    {
        return I18n.Items_QueenOfSauce_Text(this.RecipeName);
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (IsChecked)
        {
            return;
        }

        if (Game1.player.cookingRecipes.ContainsKey(this.RecipeName))
        {
            this.MarkCompleted();
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is QueenOfSauceTodoItem otherItem && this.RecipeName == otherItem.RecipeName;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.RecipeName).GetHashCode();
    }
}
