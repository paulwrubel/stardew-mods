using StardewModdingAPI.Events;

namespace AutomaticTodoList.Models;

public interface IEngine
{
    bool IsEnabled();

    IEnumerable<ITodoItem> Items();

    void UpdateItems();

    void Reset();

    void OnDayStarted(DayStartedEventArgs e);

    void OnTimeChanged(TimeChangedEventArgs e);

    void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e);

    void OnUpdateTicked(UpdateTickedEventArgs e);

    void OnMenuChanged(MenuChangedEventArgs e);

    void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug);
}
