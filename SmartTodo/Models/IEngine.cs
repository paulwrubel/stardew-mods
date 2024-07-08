namespace SmartTodo.Models
{
    public interface IEngine
    {
        List<ITodoItem> GetTodos();

        void OnTimeChanged();

        void OnUpdateTicked();

        void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug);
    }
}