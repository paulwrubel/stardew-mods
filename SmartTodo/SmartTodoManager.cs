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
        private ModConfig Config { get; }

        private IEngine[] Engines { get; }

        private SmartTodoPanel SmartTodoPanel { get; set; } = null!; // initialized in OnDayStarted

        private readonly List<ITodoItem> Items = [];

        private int GutterLength { get; }

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config)
        {
            this.Config = config;

            this.Engines = [
                new TestEngine(),
                new BirthdayEngine()
            ];
        }

        internal void OnDayStarted()
        {
            Items.Clear();
            foreach (IEngine engine in this.Engines)
            {
                Items.AddRange(engine.GetTodos());
            }

            this.SmartTodoPanel = new(new Vector2(10, 100), () => Items);
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
    }
}
