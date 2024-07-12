using AutomaticTodoList.Models;
using StardewModdingAPI.Events;

namespace AutomaticTodoList.Engines
{
    internal abstract class BaseEngine<T>(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled,
        Frequency updateFrequency,
        Frequency resetFrequency = Frequency.OnceADay
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

        public virtual void Reset()
        {
            items.Clear();
        }

        public virtual void OnDayStarted(DayStartedEventArgs e)
        {
            CheckActions(Frequency.OnceADay);

            foreach (T item in items)
            {
                item.OnDayStarted(e);
            }
        }

        public virtual void OnTimeChanged(TimeChangedEventArgs e)
        {
            CheckActions(Frequency.EveryTimeChange);

            foreach (T item in items)
            {
                item.OnTimeChanged(e);
            }
        }

        public virtual void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e)
        {
            CheckActions(Frequency.EverySecond);

            foreach (T item in items)
            {
                item.OnOneSecondUpdateTicked(e);
            }
        }

        public virtual void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            CheckActions(Frequency.EveryTick);

            foreach (T item in items)
            {
                item.OnUpdateTicked(e);
            }
        }

        public void Log(string message, StardewModdingAPI.LogLevel level = StardewModdingAPI.LogLevel.Debug) => log(message, level);

        private void CheckActions(Frequency frequency)
        {
            if (resetFrequency == frequency)
            {
                this.Reset();
            }

            if (updateFrequency == frequency)
            {
                this.UpdateItems();
            }
        }
    }
}