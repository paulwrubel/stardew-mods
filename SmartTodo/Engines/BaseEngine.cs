using SmartTodo.Models;

namespace SmartTodo.Engines
{
    internal abstract class BaseEngine(Action<string, StardewModdingAPI.LogLevel> log) : IEngine
    {

        public Action<string, StardewModdingAPI.LogLevel> LogAction { get; set; } = log;

        public abstract List<ITodoItem> GetTodos();

        public virtual void OnTimeChanged() { }

        public virtual void OnUpdateTicked() { }

        public void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug) => LogAction(message, level);
    }
}