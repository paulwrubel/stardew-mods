using AutomaticTodoList.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

internal class TodoItemTextRow(ITodoItem item, Vector2 position)
{
    private static readonly Lazy<Texture2D> lazyPixel = new(() =>
    {
        Texture2D pixel = new(Game1.graphics.GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);
        return pixel;
    });

    /// <summary>A blank pixel which can be colorized and stretched to draw geometric shapes.</summary>
    public static Texture2D Pixel => lazyPixel.Value;

    public void Draw(SpriteBatch b)
    {
        SpriteFont font = Game1.smallFont;
        Color textColor = item.IsChecked ? Color.DarkSlateGray : Color.Black;
        string text = item.Text();

        if (!item.IsChecked)
        {
            // Utility.drawBoldText(b, Text, font, position, textColor);
            Utility.drawTextWithShadow(b, text, font, position, textColor);
        }
        else
        {
            Utility.drawTextWithShadow(b, text, font, position, textColor);

            Vector2 textSize = font.MeasureString(text);
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
}
