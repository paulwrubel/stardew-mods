using StardewModdingAPI.Events;

namespace SmartTodo.Models
{
    public interface IEngine
    {
        Func<bool> IsEnabled { get; }

        IEnumerable<ITodoItem> Items { get; }

        void UpdateItems();

        void Reset();

        void OnDayStarted(DayStartedEventArgs e);

        void OnTimeChanged(TimeChangedEventArgs e);

        void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e);

        void OnUpdateTicked(UpdateTickedEventArgs e);

        void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug);
    }
}