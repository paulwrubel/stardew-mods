using Microsoft.Xna.Framework;
using StardewValley;

namespace AutomaticTodoList.Components.UI;

internal class TextRow(string text, Vector2 position)
{
    public void Draw()
    {
        Utility.drawTextWithShadow(Game1.spriteBatch, text, Game1.smallFont, position, Game1.textColor);
    }
}