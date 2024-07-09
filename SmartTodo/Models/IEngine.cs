namespace SmartTodo.Models
{
    public interface IEngine
    {
        IEnumerable<ITodoItem> Items { get; }

        void UpdateItems();

        void OnDayStarted();

        void OnTimeChanged();

        void OnUpdateTicked();

        void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug);
    }
}