using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace SmartTodo
{

    /// <summary>A todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="TodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    public class TodoItem(string text, bool isChecked = false)
    {

        /// <summary>The text of the todo item.</summary>
        public string Text { get; set; } = text;

        /// <summary>The checkbox state of the todo item.</summary>
        public bool IsChecked { get; set; } = isChecked;

        private static readonly Lazy<Texture2D> LazyPixel = new(() =>
        {
            Texture2D pixel = new(Game1.graphics.GraphicsDevice, 1, 1);
            pixel.SetData([Color.White]);
            return pixel;
        });

        /// <summary>A blank pixel which can be colorized and stretched to draw geometric shapes.</summary>
        public static Texture2D Pixel => LazyPixel.Value;

        public void draw(SpriteBatch b, Vector2 position)
        {
            SpriteFont font = Game1.smallFont;
            Color textColor = Game1.textColor;

            Utility.drawTextWithShadow(
                b,
                this.Text,
                font,
                position,
                textColor
            );

            if (this.IsChecked)
            {
                Vector2 textSize = font.MeasureString(this.Text);
                b.Draw(
                    Pixel,
                    new Rectangle(
                        (int)position.X,
                        (int)(position.Y + textSize.Y / 2),
                        (int)textSize.X,
                        1
                    ),
                    Color.White
                );
            }
        }
    }
}
