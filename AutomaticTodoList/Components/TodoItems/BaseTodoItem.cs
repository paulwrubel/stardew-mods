using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems
{

    /// <summary>A todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="BaseTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal abstract class BaseTodoItem(
        string text = "",
        bool isChecked = false,
        int priority = 0
    ) : ITodoItem
    {

        /// <summary>The text of the todo item.</summary>
        public string Text { get; set; } = text;

        /// <summary>The checkbox state of the todo item.</summary>
        public bool IsChecked { get; set; } = isChecked;

        /// <summary>The priority of the todo item. A higher number means a higher priority.</summary>
        public int Priority { get; set; } = priority;

        private static readonly Lazy<Texture2D> LazyPixel = new(() =>
        {
            Texture2D pixel = new(Game1.graphics.GraphicsDevice, 1, 1);
            pixel.SetData([Color.White]);
            return pixel;
        });

        /// <summary>A blank pixel which can be colorized and stretched to draw geometric shapes.</summary>
        public static Texture2D Pixel => LazyPixel.Value;

        public virtual void MarkCompleted()
        {
            this.IsChecked = true;
        }

        public virtual void MarkUncompleted()
        {
            this.IsChecked = false;
        }

        public virtual void OnDayStarted(DayStartedEventArgs e) { }

        public virtual void OnTimeChanged(TimeChangedEventArgs e) { }

        public virtual void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e) { }

        public virtual void OnUpdateTicked(UpdateTickedEventArgs e) { }

        public void Draw(SpriteBatch b, Vector2 position)
        {
            SpriteFont font = Game1.smallFont;
            Color textColor = this.IsChecked ? Color.DarkSlateGray : Color.Black;

            if (!this.IsChecked)
            {
                // Utility.drawBoldText(b, Text, font, position, textColor);
                Utility.drawTextWithShadow(b, Text, font, position, textColor);
            }
            else
            {
                Utility.drawTextWithShadow(b, Text, font, position, textColor);

                Vector2 textSize = font.MeasureString(this.Text);
                b.Draw(
                    Pixel,
                    new Rectangle(
                        (int)position.X,
                        (int)(position.Y + textSize.Y / 2),
                        (int)textSize.X,
                        1
                    ),
                    Color.Black
                );
            }
        }

        public abstract override bool Equals(object? obj);

        public abstract override int GetHashCode();
    }
}
