namespace SmartTodo.Models
{
    public interface IEngine
    {
        List<ITodoItem> GetTodos();

        void OnTimeChanged(Action<List<ITodoItem>>? addNewItems = null);

        void OnUpdateTicked();

        void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug);
    }
}