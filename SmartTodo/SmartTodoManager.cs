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

        private List<IEngine> Engines { get; } = [];

        private SmartTodoPanel SmartTodoPanel { get; set; } = null!; // initialized in OnDayStarted

        private readonly List<ITodoItem> Items = [];

        private readonly List<ITodoItem> CompletedItemsCache = [];

        private int GutterLength { get; }

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config)
        {
            this.Config = config;

            this.UpdateEngines();
        }

        internal void OnDayStarted()
        {
            this.ClearAndRecheckForItems(reAddCompleted: false);
            this.CompletedItemsCache.Clear();
        }

        internal void OnUpdateTicked()
        {
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

        public void UpdateEngines()
        {
            this.Engines.Clear();

            if (this.Config.CheckBirthdays)
            {
                this.Engines.Add(new BirthdayEngine(this.CompletedItemsCache.Add));
            }

            if (this.Config.CheckHarvestableCrops)
            {
                this.Engines.Add(new HarvestableCropsEngine(this.CompletedItemsCache.Add));
            }

            if (this.Config.CheckWaterableCrops)
            {
                this.Engines.Add(new WaterableCropsEngine(this.CompletedItemsCache.Add));
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

            Items.Sort((a, b) => a.Text.CompareTo(b.Text));

            this.SmartTodoPanel = new(new Vector2(10, 100), () => Items);
        }
    }
}
