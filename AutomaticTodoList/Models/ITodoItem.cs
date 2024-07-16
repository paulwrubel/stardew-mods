using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI.Events;

namespace AutomaticTodoList.Models;

public interface ITodoItem
{
    bool IsChecked { get; set; }

    TaskPriority Priority { get; set; }

    string Text();

    void OnDayStarted(DayStartedEventArgs e);

    void OnTimeChanged(TimeChangedEventArgs e);

    void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e);

    void OnUpdateTicked(UpdateTickedEventArgs e);

    void OnMenuChanged(MenuChangedEventArgs e);
}
