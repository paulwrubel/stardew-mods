using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace AutomaticTodoList.Components.UI;

internal class CenteredTextRow(string text, Vector2 position, int totalWidth)
{
    public void Draw(SpriteBatch b)
    {
        int textWidth = (int)Game1.smallFont.MeasureString(text).X;
        int xOffset = (totalWidth - textWidth) / 2;

        Vector2 centeredPosition = new(position.X + xOffset, position.Y);

        Utility.drawTextWithShadow(b, text, Game1.smallFont, centeredPosition, Game1.textColor);
    }
}
