using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTodo.Components;
using SmartTodo.Engines;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo
{
    /// <summary>Manages the Smart Todo List.</summary>
    internal sealed class SmartTodoManager
    {

        /// <summary>The config for the mod.</summary>
        public ModConfig Config { get; set; }

        private readonly Action<string, StardewModdingAPI.LogLevel> Log;

        private List<IEngine> Engines { get; } = [];

        private SmartTodoPanel SmartTodoPanel { get; set; } = null!; // initialized in OnDayStarted

        private readonly List<ITodoItem> Items = [];

        private readonly List<ITodoItem> CompletedItemsCache = [];

        private int GutterLength { get; }

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config, Action<string, StardewModdingAPI.LogLevel> log)
        {
            this.Config = config;
            this.Log = log;

            this.ResetEngines();
        }

        internal void OnDayStarted()
        {
            this.ResetEngines();
            this.ClearAndRecheckForItems(reAddCompleted: false);
            this.CompletedItemsCache.Clear();
        }

        internal void OnTimeChanged()
        {
            foreach (IEngine engine in this.Engines)
            {
                engine.OnTimeChanged(addNewItems: Items.AddRange);
            }

            foreach (ITodoItem item in Items)
            {
                item.OnTimeChanged();
            }

            // this.ClearAndRecheckForItems();
        }

        internal void OnUpdateTicked()
        {
            foreach (IEngine engine in this.Engines)
            {
                engine.OnUpdateTicked();
            }

            foreach (ITodoItem item in Items)
            {
                item.OnUpdateTicked();
            }
        }

        internal void OnRendered()
        {
            SpriteBatch b = Game1.spriteBatch;

            this.SmartTodoPanel.draw(b);
        }

        public void ResetEngines()
        {
            this.Engines.Clear();

            if (this.Config.CheckBirthdays)
            {
                this.Engines.Add(new BirthdayEngine(Log, this.CompletedItemsCache.Add));
            }

            if (this.Config.CheckHarvestableCrops)
            {
                this.Engines.Add(new HarvestableCropsEngine(Log, this.CompletedItemsCache.Add));
            }

            if (this.Config.CheckWaterableCrops)
            {
                this.Engines.Add(new WaterableCropsEngine(Log, this.CompletedItemsCache.Add));
            }

            if (this.Config.CheckHarvestableMachines)
            {
                this.Engines.Add(new HarvestableMachinesEngine(Log, this.CompletedItemsCache.Add, this.CompletedItemsCache.Remove));
            }

            if (this.Config.CheckToolPickup)
            {
                this.Engines.Add(new ToolPickupEngine(Log, this.CompletedItemsCache.Add));
            }
        }

        public void ClearAndRecheckForItems(bool reAddCompleted = true)
        {
            Items.Clear();
            foreach (IEngine engine in this.Engines)
            {
                Items.AddRange(engine.GetTodos());
            }

            if (reAddCompleted)
            {
                Items.AddRange(this.CompletedItemsCache);
            }

            Items.Sort((a, b) =>
            {
                var comp = a.Priority.CompareTo(b.Priority);
                if (comp == 0)
                {
                    comp = a.Text.CompareTo(b.Text);
                }
                return comp;
            });

            this.SmartTodoPanel = new(new Vector2(10, 100), () => Items);
        }
    }
}
