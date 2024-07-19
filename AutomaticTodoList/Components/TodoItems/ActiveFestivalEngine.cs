using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData;
using StardewValley.Locations;
using StardewValley.TokenizableStrings;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>An active festival todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="ActiveFestivalTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class ActiveFestivalTodoItem(string festivalKey, string displayName, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.Birthday)
{
    public string FestivalKey { get; } = festivalKey;

    public string DisplayName { get; } = displayName;


    public override string Text()
    {
        return FestivalKey switch
        {
            "fall27" => I18n.Items_ActiveFestival_TextWithoutArticle(DisplayName), // Sprit's Eve doesn't need an article
            _ => I18n.Items_ActiveFestival_TextWithArticle(DisplayName)
        };
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (IsChecked)
        {
            return;
        }

        if (Game1.CurrentEvent is not null && Game1.CurrentEvent.isFestival)
        {
            this.MarkCompleted();
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is ActiveFestivalTodoItem otherItem && this.FestivalKey == otherItem.FestivalKey;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.FestivalKey).GetHashCode();
    }
}
