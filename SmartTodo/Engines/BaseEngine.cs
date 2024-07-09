using SmartTodo.Models;

namespace SmartTodo.Engines
{
    internal abstract class BaseEngine<T>(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : IEngine where T : ITodoItem
    {

        public Func<bool> IsEnabled { get; } = isEnabled;

        protected readonly HashSet<T> items = [];
        public IEnumerable<ITodoItem> Items
        {
            get
            {
                if (!IsEnabled())
                {
                    return [];
                }

                UpdateItems();

                return (IEnumerable<ITodoItem>)items;
            }
        }

        public abstract void UpdateItems();

        public virtual void OnDayStarted()
        {
            foreach (T item in items)
            {
                item.OnDayStarted();
            }
        }

        public virtual void OnTimeChanged()
        {
            foreach (T item in items)
            {
                item.OnTimeChanged();
            }
        }

        public virtual void OnUpdateTicked()
        {
            foreach (T item in items)
            {
                item.OnUpdateTicked();
            }
        }

        public void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug) => log(message, level);
    }
}