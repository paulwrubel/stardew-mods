using SmartTodo.Models;
using StardewModdingAPI.Events;

namespace SmartTodo.Engines
{
    internal abstract class BaseEngine<T>(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled,
        UpdateFrequency updateFrequency
    ) : IEngine where T : ITodoItem
    {

        public bool Debug { get; set; } = false;

        public Func<bool> IsEnabled { get; } = isEnabled;

        protected readonly HashSet<T> items = [];
        public IEnumerable<ITodoItem> Items
        {
            get => (IEnumerable<ITodoItem>)items;
        }

        public abstract void UpdateItems();

        public virtual void OnDayStarted(DayStartedEventArgs e)
        {
            if (updateFrequency == UpdateFrequency.OnceADay)
            {
                this.UpdateItems();
            }

            foreach (T item in items)
            {
                item.OnDayStarted(e);
            }
        }

        public virtual void OnTimeChanged(TimeChangedEventArgs e)
        {
            if (updateFrequency == UpdateFrequency.EveryTimeChange)
            {
                this.UpdateItems();
            }

            foreach (T item in items)
            {
                item.OnTimeChanged(e);
            }
        }

        public virtual void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e)
        {
            if (updateFrequency == UpdateFrequency.EverySecond)
            {
                this.UpdateItems();
            }

            foreach (T item in items)
            {
                item.OnOneSecondUpdateTicked(e);
            }
        }

        public virtual void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            if (updateFrequency == UpdateFrequency.EveryTick)
            {
                this.UpdateItems();
            }

            foreach (T item in items)
            {
                item.OnUpdateTicked(e);
            }
        }

        public void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug) => log(message, level);
    }
}