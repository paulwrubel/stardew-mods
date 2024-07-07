using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTodo.Components;
using StardewValley;

namespace SmartTodo
{
    /// <summary>Manages the Smart Todo List.</summary>
    internal sealed class SmartTodoManager
    {

        /// <summary>The config for the mod.</summary>
        private ModConfig Config { get; }

        private SmartTodoPanel SmartTodoPanel { get; }

        private int GutterLength { get; }

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config)
        {
            this.Config = config;

            // create test Todo Items string array
            TodoItem[] items = [
                new("Go to the store"),
                new("Give Morris a present (birthday)"),
                new("Sleep!")
            ];

            this.SmartTodoPanel = new(new Vector2(10, 100), items);
        }

        internal void OnRendered()
        {
            SpriteBatch b = Game1.spriteBatch;

            this.SmartTodoPanel.draw(b);
        }
    }
}
